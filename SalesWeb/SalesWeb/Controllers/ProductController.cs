using SalesWeb.Services.ProductServices;
using SalesWeb.ViewModels.ProductViewModels;

namespace SalesWeb.Controllers;

[Controller]
public class ProductController : Controller
{
    public async Task<IActionResult> Index([FromServices] SalesWebDbContext context)
    {
        try
        {
            return View(await new GetProductService().Get(context));
        }
        catch
        {
            var error = new ErrorViewModel("C-01P - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }

    public async Task<IActionResult> GetById([FromServices] SalesWebDbContext context, int id)
    {
        if (id == null)
        {
            var error = new ErrorViewModel("C-02P - Product Identification can not be null.");
            return RedirectToAction(nameof(Error), error);
        }

        try
        {
            View(await new GetByIdProductService().GetById(context, id));
            return RedirectToAction(nameof(Index));
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
    public async Task<IActionResult> Post([FromServices] SalesWebDbContext context, PostProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-05C - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }
        
        try
        {
            View(await new PostProductService().Post(context, model));
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            var error = new ErrorViewModel("C-07P - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }

    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, int id)
    {
        if (id == null)
        {
            var error = new ErrorViewModel("C-08P - Product Identification can not be null.");
            return RedirectToAction(nameof(Error), error);
        }

        try
        {
            return View(await new PutProductService().GetById(context, id));
        }
        catch
        {
            var errorView = new ErrorViewModel("C-09P - Can not find Product.");
            return RedirectToAction(nameof(Error), errorView);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, int id,
        PutProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-10P - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }

        try
        {
            View(await new PutProductService().Put(context, id, model));
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            var error = new ErrorViewModel("C-13P - Internal server error.");
            error.Errors.Add(ex.Message);
            return RedirectToAction(nameof(Error), error);
        }
    }

    public async Task<IActionResult> Delete([FromServices] SalesWebDbContext context, int id)
    {
        if (id == null)
        {
            var error = new ErrorViewModel("C-14P - Product Identification can not be null.");
            return RedirectToAction(nameof(Error), error);
        }

        try
        {
            return View(await new DeleteProductService().GetById(context, id)); 
        }
        catch (Exception ex)
        {
            var error = new ErrorViewModel("C-16P - Internal server error.");
            error.Errors.Add(ex.Message);
            return RedirectToAction(nameof(Error), error);
        }
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed([FromServices] SalesWebDbContext context, int id)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-17C - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }

        try
        {
            View(await new DeleteProductService().Delete(context, id));
            return RedirectToAction(nameof(Index));
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