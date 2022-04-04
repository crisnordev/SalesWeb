using Microsoft.AspNetCore.Mvc.Rendering;
using SalesWeb.ViewModels.SaleViewModels;

namespace SalesWeb.Controllers;

[Controller]
public class SaleController : Controller
{
    public async Task<IActionResult> Index([FromServices] SalesWebDbContext context)
    {
        var salesContext = await context.Sales.AsNoTracking()
            .Include(x => x.Seller)
            .Include(x => x.Customer)
            .ToListAsync();
        var sales = salesContext.Select(x => (GetSaleViewModel) x).ToList();

        return View(sales);
    }

    public async Task<IActionResult> GetById([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null) return NotFound();

        var sale = new GetSaleViewModel();
        sale = await context.Sales.AsNoTracking()
            .Include(x => x.Seller)
            .Include(x => x.Customer)
            .Include(x => x.SoldProducts)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (sale == null) return NotFound();
        
        return View(sale);
    }

    public async Task<IActionResult> Post([FromServices] SalesWebDbContext context, PostSaleViewModel model)
    {
        ViewData["CustomerId"] = new SelectList(context.Customers, "Id", "Name");
        ViewData["SellerId"] = new SelectList(context.Sellers, "Id", "Name");
        ViewData["ProductId"] = new SelectList(context.Products, "Id", "Name");

        return View(model);
    }

    [HttpPost, ActionName("Post")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> ConfirmPost([FromServices] SalesWebDbContext context, PostSaleViewModel model)
    {
        if (ModelState.IsValid)
        {
            var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == model.CustomerId);
            if (customer == null) return NotFound();
            var seller = await context.Sellers.FirstOrDefaultAsync(x => x.Id == model.SellerId);
            if (seller == null) return NotFound();
            var sale = new Sale
            {
                Id = Guid.NewGuid(),
                Customer = customer,
                Seller = seller,
                TotalAmount = 0,
                SoldProducts = new List<SoldProduct>()
            };
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == model.ProductId);
            if (product == null) return NotFound();
            sale.TotalAmount = model.ProductQuantity * product.Price;
            var soldProduct = new SoldProduct
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id.ToString(),
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
                return StatusCode(400, "C-01S - An issue has happen. Check information, and try again.");
            }
        }
        return View();
    }
    
    public async  Task<IActionResult> Put([FromServices] SalesWebDbContext context, Guid id,  PutSaleViewModel model)
    {
        ViewData["CustomerId"] = new SelectList(context.Customers, "Id", "Name");
        ViewData["SellerId"] = new SelectList(context.Sellers, "Id", "Name");
        ViewData["ProductId"] = new SelectList(context.Products, "Id", "Name");

        if (id == null) return NotFound();
        
        var sale = await context.Sales.AsNoTracking()
            .Include(x => x.Seller)
            .Include(x => x.Customer)
            .Include(x => x.SoldProducts)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (sale == null) return NotFound();

        model = sale;

        return View(model);
    }
    
    [HttpPost, ActionName("Put")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PutSaleProduct([FromServices] SalesWebDbContext context, Guid id, PutSaleViewModel model)
    {
        if (ModelState.IsValid)
        {
            var sale = await context.Sales.FirstOrDefaultAsync(x => x.Id == id);
            if (sale == null) return NotFound();
            
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == model.ProductId);
            if (product == null) return NotFound();
            sale.TotalAmount += model.ProductQuantity * product.Price;
            var soldProduct = new SoldProduct
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id.ToString(),
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
                return StatusCode(500, "C-02S - Unable to edit sale.");
            }
            catch (Exception)
            {
                return StatusCode(500, "C-03S - Internal server error.");
            }
        }
        return View(model);
    }


    public async Task<IActionResult> Delete([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null) return NotFound();

        var sale = new GetSaleViewModel();
        sale = await context.Sales.AsNoTracking()
            .Include(x => x.Seller)
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (sale == null) return NotFound();

        return View(sale);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed([FromServices] SalesWebDbContext context, Guid id)
    {
        var sale = await context.Sales
            .Include(x => x.Seller)
            .Include(x => x.Customer)
            .Include(x => x.SoldProducts)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (ModelState.IsValid)
        {
            try
            {
                var soldProducts = await context.SoldProducts.ToListAsync();
                foreach (var product in sale.SoldProducts)
                    soldProducts.Remove(product);
                context.Sales.Remove(sale!);
                await context.SaveChangesAsync();
                View(sale);

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Unable to delete Sale.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        return View();
    }
}