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
            var sales = salesContext.Select(item => new GetSaleViewModel(
                item.SaleId, item.Customer.Name.FullName, item.Seller.Name.FullName, item.TotalAmount))
                .ToList();
            
            return View(sales);
        }
        catch(Exception ex)
        {
            var error = new ErrorViewModel("C-01SA - Internal server error.");
            error.Errors.Add(ex.Message);
            return RedirectToAction(nameof(Error), error);
        }
    }

    public async Task<IActionResult> GetById([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null)
            return RedirectToAction(nameof(Error), new ErrorViewModel("C-02SA - Sale Identification can not be null."));

        try
        {
            var saleContext = await context.Sales.AsNoTracking()
                .Include(x => x.Seller)
                .Include(x => x.Customer)
                .Include(x => x.SoldProducts)
                .FirstOrDefaultAsync(x => x.SaleId == id);
            if (saleContext == null)
                return RedirectToAction(nameof(Error), new ErrorViewModel("C-03SA - Can not find Sale."));
            
            var sale = new GetSaleViewModel(saleContext.SaleId, saleContext.Customer.Name.FullName, saleContext.Seller.Name.FullName,
                saleContext.TotalAmount);
            foreach (var item in saleContext.SoldProducts)
                sale.SoldProducts.Add(new SoldProduct(item.SoldProductId, item.ProductId, new ProductName(item.ProductName.ProductFullName),
                    item.Quantity, item.Price));

            return View(sale);
        }
        catch(Exception ex)
        {
            var error = new ErrorViewModel("C-04SA - Internal server error.");
            error.Errors.Add(ex.Message);
            return RedirectToAction(nameof(Error), error);
        }
    }

    public async Task<IActionResult> Post([FromServices] SalesWebDbContext context)
    {
        try
        {
            ViewData["CustomerId"] = new SelectList(context.Customers, "CustomerId", "Name", "FullName" );
            ViewData["SellerId"] = new SelectList(context.Sellers, "SellerId", "Name", "FullName");
            ViewData["ProductId"] = new SelectList(context.Products, "ProductId", "ProductName");
            return View(new PostSaleViewModel());
        }
        catch (Exception ex)
        {
            var error = new ErrorViewModel("C-05SA - Customer, Seller, or Product must not be empty.");
            error.Errors.Add(ex.Message);
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
            TotalAmount = model.ProductQuantity * product.Price
        };

        sale.SoldProducts = new List<SoldProduct>
        {
            new(Guid.NewGuid(), product.ProductId, new ProductName(product.ProductName.ProductFullName),
                model.ProductQuantity, product.Price)
        };

        try
        {
            await context.SoldProducts.AddRangeAsync(sale.SoldProducts);
            await context.Sales.AddAsync(sale);
            await context.SaveChangesAsync();

            View(model);
            return RedirectToAction(nameof(GetById), new {id = sale.SaleId});
        }
        catch (Exception ex)
        {
            var error = new ErrorViewModel("C-11SA - Internal server error.");
            error.Errors.Add(ex.Message);
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

            var putSaleViewModel = new PutSaleViewModel(sale.SaleId,sale.Customer.Name.FullName, sale.Seller.Name.FullName, 0,
                0, sale.TotalAmount);
            putSaleViewModel.SoldProducts = (IReadOnlyCollection<SoldProduct>)sale.SoldProducts;

            ViewData["ProductId"] = new SelectList(context.Products, "ProductId", "ProductName");
            
            return View(putSaleViewModel);
        }
        catch(Exception ex)
        {
            var error = new ErrorViewModel("C-14SA - Internal server error.");
            error.Errors.Add(ex.Message);
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
            .Include(x => x.Seller)
            .Include(x => x.Customer)
            .Include(x => x.SoldProducts)
            .FirstOrDefaultAsync(x => x.SaleId == id);
        
        var product = await context.Products.FirstOrDefaultAsync(x => x.ProductId == model.ProductId);
        
        var soldProduct = new SoldProduct(Guid.NewGuid(), product.ProductId, new ProductName(
            product.ProductName.ProductFullName), model.ProductQuantity, product.Price);

        sale.TotalAmount += model.ProductQuantity * product.Price;
        sale.SoldProducts.Add(soldProduct);

        try
        {
            await context.SoldProducts.AddAsync(soldProduct);
            context.Sales.Update(sale);
            await context.SaveChangesAsync();

            View(model);
            return RedirectToAction(nameof(GetById), new { id });
        }
        catch (Exception ex)
        {
            var error = new ErrorViewModel("C-20SA - Internal server error.");
            error.Errors.Add(ex.Message);
            return RedirectToAction(nameof(Error), error);
        }
    }

    public async Task<IActionResult> Delete([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null)
            return RedirectToAction(nameof(Error), new ErrorViewModel("C-21SA - Sale Identification can not be null."));

        var saleContext = await context.Sales.AsNoTracking()
            .Include(x => x.Seller)
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.SaleId == id);
        if (saleContext == null)
            return RedirectToAction(nameof(Error), new ErrorViewModel("C-22SA - Can not find Sale."));
            
        var sale = new PutSaleViewModel(saleContext.SaleId, saleContext.Customer.Name.FullName, saleContext.Seller.Name.FullName,
            0, 0, saleContext.TotalAmount);
        return View(sale);
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

        var sale = await context.Sales.AsNoTracking()
            .Include(x => x.Seller)
            .Include(x => x.Customer)
            .Include(x => x.SoldProducts)
            .FirstOrDefaultAsync(x => x.SaleId == id);
        
        try
        {
            context.Sales.Remove(sale);
            await context.SaveChangesAsync();
            View();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            var error = new ErrorViewModel("C-26SA - Unable to delete this Sale, check data, and try again.");
            error.Errors.Add(ex.Message);
            return RedirectToAction(nameof(Error), error);
        }

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(ErrorViewModel error) => View(new ErrorViewModel(error.Errors));
}