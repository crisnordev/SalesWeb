using Microsoft.AspNetCore.Mvc.Rendering;
namespace SalesWeb.Controllers;

[Controller]
public class SaleController : Controller
{
    public async Task<IActionResult> Index([FromServices] SalesWebDbContext context)
    {
        try
        {
            var salesContext = await context.Sales.AsNoTracking()
                .Include(x => x.Seller)
                .Include(x => x.Customer)
                .ToListAsync();
            var sales = salesContext.Select(x => (GetSaleViewModel) x).ToList();
            return View(sales);
        }
        catch (ArgumentNullException)
        {
            var error = new ErrorViewModel("C-00SA - Empty Sale list.");
            return RedirectToAction(nameof(Error), error);
        }
        catch
        {
            var error = new ErrorViewModel("C-01SA - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }

    public async Task<IActionResult> GetById([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null)
        {
            var error = new ErrorViewModel("C-02SA - Sale Identification can not be null.");
            return RedirectToAction(nameof(Error), error);
        }

        try
        {
            var sale = new GetSaleViewModel();
            sale = await context.Sales.AsNoTracking()
                .Include(x => x.Seller)
                .Include(x => x.Customer)
                .Include(x => x.SoldProducts)
                .FirstOrDefaultAsync(x => x.SaleId == id);
            if (sale != null) return View(sale);
            var error = new ErrorViewModel("C-03SA - Can not find Sale.");
            return RedirectToAction(nameof(Error), error);
        }
        catch
        {
            var error = new ErrorViewModel("C-04SA - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }

    public async Task<IActionResult> Post([FromServices] SalesWebDbContext context)
    {
        try
        {
            ViewData["CustomerId"] = new SelectList(context.Customers, "CustomerId", "Name", "CompleteName" );
            ViewData["SellerId"] = new SelectList(context.Sellers, "SellerId", "Name", "CompleteName");
            ViewData["ProductId"] = new SelectList(context.Products, "ProductId", "ProductName");
            return View(new PostSaleViewModel());
        }
        catch
        {
            var error = new ErrorViewModel("C-05SA - Customer, Seller, or Product must not be empty.");
            return RedirectToAction(nameof(Error), error);
        }
    }

    [HttpPost]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Post([FromServices] SalesWebDbContext context, PostSaleViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-06SA - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }

        var customer = await context.Customers.FirstOrDefaultAsync(x => x.CustomerId == model.CustomerId);
        var seller = await context.Sellers.FirstOrDefaultAsync(x => x.SellerId == model.SellerId);
        var product = await context.Products.FirstOrDefaultAsync(x => x.ProductId == model.ProductId);

        var sale = new Sale
        {
            SaleId = Guid.NewGuid(),
            Customer = customer,
            Seller = seller,
            TotalAmount = model.ProductQuantity * product.Price,
            SoldProduct = new SoldProduct(Guid.NewGuid(), product.ProductId, product.ProductName, model.ProductQuantity, product.Price)
        };
        sale.SoldProducts.Add(sale.SoldProduct);

        try
        {
            await context.Sales.AddAsync(sale);
            await context.SoldProducts.AddAsync(sale.SoldProduct);
            await context.SaveChangesAsync();
            Ok(sale);
            Ok(sale.SoldProduct);

            View(model);
            return RedirectToAction(nameof(Put), new {sale.SaleId});
        }
        catch (DbUpdateException)
        {
            var error = new ErrorViewModel("C-10SA - Some error has occurred, try again.");
            return RedirectToAction(nameof(Error), error);
        }
        catch
        {
            var error = new ErrorViewModel("C-11SA - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }

    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, Guid id)
    {
        try
        {
            var sale = await context.Sales.AsNoTracking()
                .Include(x => x.Seller)
                .Include(x => x.Customer)
                .Include(x => x.SoldProducts)
                .FirstOrDefaultAsync(x => x.SaleId == id);

            var putSaleViewModel = new PutSaleViewModel(sale.Customer.Name.CompleteName, sale.Seller.Name.CompleteName, 
                sale.TotalAmount);
            putSaleViewModel.SoldProducts.AddRange(sale.SoldProducts);
            
            ViewData["ProductId"] = new SelectList(context.Products, "ProductId", "ProductName");
            
            return View(putSaleViewModel);
        }
        catch
        {
            var error = new ErrorViewModel("C-14SA - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, Guid id, PutSaleViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-16SA - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }

        var sale = await context.Sales
            .Include(x => x.Customer)
            .Include(x => x.Seller)
            .Include(x => x.SoldProducts)
            .FirstOrDefaultAsync(x => x.SaleId == id);

        var product = await context.Products.FirstOrDefaultAsync(x => x.ProductId == model.ProductId);

        sale.TotalAmount += model.ProductQuantity * product.Price;
        
        var soldProduct = new SoldProduct(Guid.NewGuid(), product.ProductId, product.ProductName,
            model.ProductQuantity, product.Price);
        sale.SoldProducts.Add(soldProduct);

        try
        {
            context.Sales.Update(sale);
            await context.SoldProducts.AddAsync(soldProduct);
            await context.SaveChangesAsync();
            Ok(sale);
            Ok(soldProduct);

            View(model);
            return RedirectToAction(nameof(Put), new {sale.SaleId});
        }
        catch (DbUpdateException)
        {
            var error = new ErrorViewModel("C-19SA - Unable to update this Sale, check data, and try again.");
            return RedirectToAction(nameof(Error), error);
        }
        catch
        {
            var error = new ErrorViewModel("C-20SA - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }

    public async Task<IActionResult> Delete([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null)
        {
            var error = new ErrorViewModel("C-21SA - Sale Identification can not be null.");
            return RedirectToAction(nameof(Error), error);
        }

        GetSaleViewModel sale = await context.Sales.AsNoTracking()
            .Include(x => x.Seller)
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.SaleId == id);
        if (sale != null) return View(sale);
        var errorSale = new ErrorViewModel("C-22SA - Can not find Sale.");
        return RedirectToAction(nameof(Error), errorSale);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed([FromServices] SalesWebDbContext context, Guid id)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-23SA - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }

        var sale = await context.Sales
            .Include(x => x.Seller)
            .Include(x => x.Customer)
            .Include(x => x.SoldProducts)
            .FirstOrDefaultAsync(x => x.SaleId == id);
        
        var soldProducts = await context.SoldProducts.ToListAsync();
        
        try
        {
            foreach (var product in sale.SoldProducts)
                soldProducts.Remove(product);

            context.Sales.Remove(sale!);
            await context.SaveChangesAsync();
            View(sale);
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            var error = new ErrorViewModel("C-26SA - Unable to delete this Sale, check data, and try again.");
            return RedirectToAction(nameof(Error), error);
        }
        catch
        {
            var error = new ErrorViewModel("C-27SA - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(ErrorViewModel error) => View(new ErrorViewModel(error.Errors));
}