using Microsoft.AspNetCore.Mvc.Rendering;
using SalesWeb.ViewModels.SaleViewModels;

namespace SalesWeb.Controllers;

[Controller]
public class SaleController : Controller
{
    [HttpGet]
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

    [HttpGet]
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
                .FirstOrDefaultAsync(x => x.Id == id);
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
    
    public async Task<IActionResult> Post([FromServices] SalesWebDbContext context, PostSaleViewModel model)
    {
        try
        {
            ViewData["CustomerId"] = new SelectList(context.Customers, "CustomerId", "FirstName");
            ViewData["SellerId"] = new SelectList(context.Sellers, "SellerId", "FirstName");
            ViewData["ProductId"] = new SelectList(context.Products, "ProductId", "Name");
            return View(model);
        }
        catch
        {
            var error = new ErrorViewModel("C-05SA - Customer, Seller, or Product must not be empty.");
            return RedirectToAction(nameof(Error), error);
        }
    }

    [HttpPost, ActionName("Post")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> ConfirmPost([FromServices] SalesWebDbContext context, PostSaleViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-06SA - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }

        var customer = await context.Customers.FirstOrDefaultAsync(x => x.CustomerId == model.CustomerId);
        if (customer == null)
        {
            var error = new ErrorViewModel("C-07SA - Can not find Customer.");
            return RedirectToAction(nameof(Error), error);
        }

        var seller = await context.Sellers.FirstOrDefaultAsync(x => x.SellerId == model.SellerId);
        if (seller == null)
        {
            var error = new ErrorViewModel("C-08SA - Can not find Seller.");
            return RedirectToAction(nameof(Error), error);
        }

        var sale = new Sale
        {
            Customer = customer,
            Seller = seller,
            TotalAmount = 0,
            SoldProducts = new List<SoldProduct>()
        };
        var product = await context.Products.FirstOrDefaultAsync(x => x.ProductId == model.ProductId);
        if (product == null)
        {
            var error = new ErrorViewModel("C-09SA - Can not find Product.");
            return RedirectToAction(nameof(Error), error);
        }

        sale.TotalAmount = model.ProductQuantity * product.Price;
        var soldProduct = new SoldProduct
        {
            ProductId = product.ProductId.ToString(),
            Name = product.Name,
            Quantity = model.ProductQuantity,
            Price = product.Price,
            Sales = new List<Sale>()
        };
        sale.SoldProducts.Add(soldProduct);

        try
        {
            await context.Sales.AddAsync(sale);
            await context.SoldProducts.AddAsync(soldProduct);
            await context.SaveChangesAsync();
            Ok(sale);
            Ok(soldProduct);

            View(model);
            return RedirectToAction(nameof(Put), new {sale.Id});
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
    
    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, Guid id, PutSaleViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-12SA - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }

        if (id == null)
        {
            var error = new ErrorViewModel("C-13SA - Sale Identification can not be null.");
            return RedirectToAction(nameof(Error), error);
        }

        try
        {
            ViewData["CustomerId"] = new SelectList(context.Customers, "CustomerId", "FirstName");
            ViewData["SellerId"] = new SelectList(context.Sellers, "SellerId", "FirstName");
            ViewData["ProductId"] = new SelectList(context.Products, "ProductId", "Name");
        }
        catch
        {
            var error = new ErrorViewModel("C-14SA - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }

        var sale = await context.Sales.AsNoTracking()
            .Include(x => x.Seller)
            .Include(x => x.Customer)
            .Include(x => x.SoldProducts)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (sale == null)
        {
            var error = new ErrorViewModel("C-15SA - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }

        model = sale;
        return View(model);
    }

    [HttpPost, ActionName("Put")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PutSaleProduct([FromServices] SalesWebDbContext context, Guid id,
        PutSaleViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-16SA - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }

        var sale = await context.Sales.FirstOrDefaultAsync(x => x.Id == id);
        if (sale == null)
        {
            var error = new ErrorViewModel("C-17SA - Can not find Sale.");
            return RedirectToAction(nameof(Error), error);
        }

        var product = await context.Products.FirstOrDefaultAsync(x => x.ProductId == model.ProductId);
        if (product == null)
        {
            var error = new ErrorViewModel("C-18SA - Can not find Product.");
            return RedirectToAction(nameof(Error), error);
        }

        sale.TotalAmount += model.ProductQuantity * product.Price;
        var soldProduct = new SoldProduct
        {
            ProductId = product.ProductId.ToString(),
            Name = product.Name,
            Quantity = model.ProductQuantity,
            Price = product.Price
        };
        sale.SoldProducts.Add(soldProduct);

        try
        {
            context.Sales.Update(sale);
            await context.SoldProducts.AddAsync(soldProduct);
            await context.SaveChangesAsync();
            Ok(sale);
            Ok(soldProduct);

            View(model);
            return RedirectToAction(nameof(Put), new {sale.Id});
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
            .FirstOrDefaultAsync(x => x.Id == id);
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
            .FirstOrDefaultAsync(x => x.Id == id);
        if (sale == null)
        {
            var error = new ErrorViewModel("C-24SA - Can not find Sale.");
            return RedirectToAction(nameof(Error), error);
        }

        var soldProducts = await context.SoldProducts.ToListAsync();
        try
        {
            foreach (var product in sale.SoldProducts)
            {
                if (product == null)
                {
                    var error = new ErrorViewModel("C-25SA - Can not find Product.");
                    return RedirectToAction(nameof(Error), error);
                }

                soldProducts.Remove(product);
            }

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