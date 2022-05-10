using SalesWeb.ViewModels.ProductViewModels;

namespace SalesWeb.Controllers;

[Controller]
public class ProductController : Controller
{
    public async Task<IActionResult> Index([FromServices] SalesWebDbContext context)
    {
        try
        {
            var productsContext = await context.Products.AsNoTracking().ToListAsync();
            var products = productsContext.Select(x => (GetProductViewModel) x).ToList();
            return View(products);
        }
        catch
        {
            var error = new ErrorViewModel("C-01P - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }

    public async Task<IActionResult> GetById([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null)
        {
            var error = new ErrorViewModel("C-02P - Product Identification can not be null.");
            return RedirectToAction(nameof(Error), error);
        }

        try
        {
            GetProductViewModel product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == id);
            if (product != null) return View(product);
            var error = new ErrorViewModel("C-03P - Can not find Product.");
            return RedirectToAction(nameof(Error), error);
        }
        catch
        {
            var error = new ErrorViewModel("C-04P - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }

    public IActionResult Post()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Post([FromServices] SalesWebDbContext context, EditorProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-05C - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }

        var product = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = model.ProductName,
            Price = model.Price
        };
        try
        {
            context.Add(product);
            await context.SaveChangesAsync();

            View(model);
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            var error = new ErrorViewModel("C-06P - This Product has already been added.");
            return RedirectToAction(nameof(Error), error);
        }
        catch
        {
            var error = new ErrorViewModel("C-07P - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }

    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null)
        {
            var error = new ErrorViewModel("C-08P - Product Identification can not be null.");
            return RedirectToAction(nameof(Error), error);
        }

        try
        {
            EditorProductViewModel product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == id);
            return View(product);
        }
        catch
        {
            var errorView = new ErrorViewModel("C-09P - Can not find Product.");
            return RedirectToAction(nameof(Error), errorView);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, Guid id,
        EditorProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-10P - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }

        var product = await context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
        if (product == null)
        {
            var error = new ErrorViewModel("C-11P - Can not find Product.");
            return RedirectToAction(nameof(Error), error);
        }

        product.ProductName = model.ProductName;
        product.Price = model.Price;
        try
        {
            context.Update(product);
            await context.SaveChangesAsync();

            Ok(product);
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            var error = new ErrorViewModel("C-12P - Unable to update this Product, check data, and try again.");
            return RedirectToAction(nameof(Error), error);
        }
        catch
        {
            var error = new ErrorViewModel("C-13P - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }

    public async Task<IActionResult> Delete([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null)
        {
            var error = new ErrorViewModel("C-14P - Product Identification can not be null.");
            return RedirectToAction(nameof(Error), error);
        }

        try
        {
            GetProductViewModel product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == id);
            if (product != null) return View(product);
            var error = new ErrorViewModel("C-15P - Can not find Product.");
            return RedirectToAction(nameof(Error), error);
        }
        catch
        {
            var error = new ErrorViewModel("C-16P - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed([FromServices] SalesWebDbContext context, Guid id)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-17C - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }

        var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == id);
        if (product == null)
        {
            var error = new ErrorViewModel("C-18C - Can not find Product.");
            return RedirectToAction(nameof(Error), error);
        }

        try
        {
            context.Products.Remove(product!);
            await context.SaveChangesAsync();

            Ok(product);
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            var error = new ErrorViewModel("C-19P - Can not delete this Product.");
            return RedirectToAction(nameof(Error), error);
        }
        catch
        {
            var error = new ErrorViewModel("C-20P - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(ErrorViewModel error) => View(new ErrorViewModel(error.Errors));
}