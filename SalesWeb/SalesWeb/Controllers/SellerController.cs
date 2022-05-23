using SalesWeb.Services.SellerServices;
using SalesWeb.ViewModels.SellerViewModels;

namespace SalesWeb.Controllers;

[Controller]
public class SellerController : Controller
{
    public async Task<IActionResult> Index([FromServices] SalesWebDbContext context)
    {
        try
        {
            return View(await new GetSellerService().Get(context));
        }
        catch (Exception exception)
        {
            var error = new ErrorViewModel("C-01SE - Internal server error.");
            error.Errors.Add(exception.Message);
            return RedirectToAction(nameof(Error), error);
        }
    }

    public async Task<IActionResult> GetById([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null)
        {
            var error = new ErrorViewModel("C-02SE - Seller Identification can not be null.");
            return RedirectToAction(nameof(Error), error);
        }
        try
        {
            return View(await new GetByIdSellerService().GetById(context, id));
        }
        catch (Exception exception)
        {
            var error = new ErrorViewModel("C-04SE - Internal server error.");
            error.Errors.Add(exception.Message);
            return RedirectToAction(nameof(Error), error);
        }
    }

    public IActionResult Post()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Post([FromServices] SalesWebDbContext context, PostSellerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-05SE - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }
        try
        {
            View(await new PostSellerService().Post(context, model));
            return RedirectToAction(nameof(Index));
        }
        catch (Exception exception)
        {
            var error = new ErrorViewModel("C-07SE - Internal server error.");
            error.Errors.Add(exception.Message);
            return RedirectToAction(nameof(Error), error);
        }
    }

    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null)
        {
            var error = new ErrorViewModel("C-08SE - Seller Identification can not be null.");
            return RedirectToAction(nameof(Error), error);
        }
        try
        {
            return View(await new PutSellerService().GetById(context, id));
        }
        catch (Exception exception)
        {
            var errorView = new ErrorViewModel("C-09SE - Can not find Seller.");
            errorView.Errors.Add(exception.Message);
            return RedirectToAction(nameof(Error), errorView);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, PutSellerViewModel model, Guid id)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-10SE - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }
        try
        {
            View(await new PutSellerService().Put(context, id, model));
            return RedirectToAction(nameof(Index));
        }
        catch (Exception exception)
        {
            var error = new ErrorViewModel("C-13SE - Internal server error.");
            error.Errors.Add(exception.Message);
            return RedirectToAction(nameof(Error), error);
        }
    }

    public async Task<IActionResult> Delete([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null)
        {
            var error = new ErrorViewModel("C-14SE - Seller Identification can not be null.");
            return RedirectToAction(nameof(Error), error);
        }
        try
        {
            return View(await new DeleteSellerService().GetById(context, id));
        }
        catch (Exception exception)
        {
            var error = new ErrorViewModel("C-16SE - Internal server error.");
            error.Errors.Add(exception.Message);
            return RedirectToAction(nameof(Error), error);
        }
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed([FromServices] SalesWebDbContext context, Guid id)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-17SE - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }
        try
        {
            View(await new DeleteSellerService().Delete(context, id));
            return RedirectToAction(nameof(Index));
        }
        catch (Exception exception)
        {
            var error = new ErrorViewModel("C-20SE - Internal server error.");
            error.Errors.Add(exception.Message);
            return RedirectToAction(nameof(Error), error);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(ErrorViewModel error)
    {
        return View(new ErrorViewModel
        {
            Errors = error.Errors,
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}