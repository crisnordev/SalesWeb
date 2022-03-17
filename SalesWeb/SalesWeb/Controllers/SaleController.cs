using SalesWeb.Models.ViewModels;

namespace SalesWeb.Controllers;

[Controller]
public class SaleController : Controller
{
    public async Task<IActionResult> Index([FromServices] SalesWebDbContext context)
    {
        var sales = await context.Sales.AsNoTracking()
            .Include(x => x.Seller)
            .Include(x => x.Customer)
            .ToListAsync();

        return View(sales);
    }

    public async Task<IActionResult> GetById([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null) return NotFound();

        var sale = await context.Sales.AsNoTracking()
            .Include(x => x.Seller)
            .Include(x => x.Customer)
            .Include(x => x.SoldProducts)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (sale == null) return NotFound();
        var saleView = new ListSaleViewModel
        {
            SaleId = id,
            CustomerName = sale.Customer.Name,
            SellerName = sale.Seller.Name,
            TotalAmount = sale.TotalAmount,
            SoldProducts = sale.SoldProducts
        };

        return View(saleView);
    }

    public IActionResult Post()
    {
        return View();
    }

    [HttpPost]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Post([FromServices] SalesWebDbContext context, 
        [Bind("SellerId,CustomerId,ProductId,ProductQuantity")] SaleViewModel model)
    {
        if (ModelState.IsValid)
        {
            var seller = await context.Sellers.FirstOrDefaultAsync(x => x.Id == model.SellerId);
            if (seller == null) return NotFound();
            var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == model.CustomerId);
            if (customer == null) return NotFound();
            var sale = new Sale
            {
                Id = Guid.NewGuid(),
                Seller = seller,
                Customer = customer,
                TotalAmount = 0,
                SoldProducts = new List<SoldProduct>()
            };
            var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.ProductId);
            if (product == null) return NotFound();
            sale.TotalAmount = model.ProductQuantity * product.Price;
            var soldProduct = new SoldProduct
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Quantity = model.ProductQuantity,
                Price = product.Price,
                Sales = new List<Sale>()
            };
            sale.SoldProducts.Add(soldProduct);

            try
            {
                await context.SoldProducts.AddAsync(soldProduct);
                await context.Sales.AddAsync(sale);
                await context.SaveChangesAsync();
                
                View(model);
                return RedirectToAction(nameof(Put), new
                {
                    sale.Id
                });
            }
            catch (DbUpdateException)
            {
                return StatusCode(400, "Error.");
            }
        }

        return View();
    }
    
    public async  Task<IActionResult> Put([FromServices] SalesWebDbContext context, Guid id,  PutSaleViewModel model)
    {
        if (id == null) return NotFound();
        
        var sale = await context.Sales.AsNoTracking()
            .Include(x => x.Seller)
            .Include(x => x.Customer)
            .Include(x => x.SoldProducts)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (sale == null) return NotFound();

        model.SaleId = id;
        model.CustomerId = sale.Customer.Id;
        model.CustomerName = sale.Customer.Name;
        model.SellerId = sale.Seller.Id;
        model.SellerName = sale.Seller.Name;
        model.TotalAmount = sale.TotalAmount;
        model.SoldProducts = sale.SoldProducts;
        model.ProductId = Guid.Empty;
        model.ProductQuantity = 0;

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
            
            var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.ProductId);
            if (product == null) return NotFound();
            sale.TotalAmount += model.ProductQuantity * product.Price;
            var soldProduct = new SoldProduct
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Quantity = model.ProductQuantity,
                Price = product.Price
            };
            sale.SoldProducts.Add(soldProduct);
            
            try
            {
                await context.SoldProducts.AddAsync(soldProduct);
                context.Sales.Update(sale);
                await context.SaveChangesAsync();

                View(model);
                return RedirectToAction(nameof(Put), new
                {
                    sale.Id
                });
            }
            catch (DbUpdateException)
            {
                return StatusCode(400, "Error.");
            }
        }

        return View(model);
    }


    public async Task<IActionResult> Delete([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null) return NotFound();

        var sale = await context.Sales.AsNoTracking()
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
                return StatusCode(500, "Unable to update product.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        return View();
    }
}