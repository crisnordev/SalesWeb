using SalesWeb.Services.CustomerServices;
using SalesWeb.ViewModels.CustomerViewModels;

namespace SalesWeb.Controllers;

[Controller]
public class CustomerController : Controller
{
    public async Task<IActionResult> Index([FromServices] SalesWebDbContext context)
    {
        try
        {
            return View(await new GetCustomerService().Get(context));
        }
        catch (Exception exception)
        {
            var error = new ErrorViewModel("C-01C - Internal server error.");
            error.Errors.Add(exception.Message);
            return RedirectToAction(nameof(Error), error);
        }
    }

    public async Task<IActionResult> GetById([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null)
        {
            var error = new ErrorViewModel("C-02C - Customer Identification can not be null.");
            return RedirectToAction(nameof(Error), error);
        }

        try
        {
            return View(await new GetByIdCustomerService().GetById(context, id));
        }
        catch (Exception exception)
        {
            var error = new ErrorViewModel("C-03C - Internal server error.");
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
    public async Task<IActionResult> Post([FromServices] SalesWebDbContext context, PostCustomerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-04C - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }

        try
        {
            View(await new PostCustomerService().Post(context, model));
            return RedirectToAction(nameof(Index));
        }
        // catch (DbUpdateException)
        // {
        //     var error = new ErrorViewModel("C-05C - This e-mail has already been used.");
        //     return RedirectToAction(nameof(Error), error);
        // }
        catch (Exception exception)
        {
            var error = new ErrorViewModel("C-06C - Internal server error.");
            error.Errors.Add(exception.Message);
            return RedirectToAction(nameof(Error), error);
        }
    }

    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null)
        {
            var error = new ErrorViewModel("C-07C - Customer Identification can not be null.");
            return RedirectToAction(nameof(Error), error);
        }

        try
        {
            return View(await new PutCustomerService().GetById(context, id));
        }
        catch (Exception exception)
        {
            var errorView = new ErrorViewModel("C-08C - Can not find Customer.");
            errorView.Errors.Add(exception.Message);
            return RedirectToAction(nameof(Error), errorView);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, Guid id,
        PutCustomerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-09C - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }
        try
        {
            Ok(await new PutCustomerService().Put(context, id, model));
            return RedirectToAction(nameof(Index));
        }
        // catch (DbUpdateException)
        // {
        //     var error = new ErrorViewModel("C-12C - Unable to update this Customer, check data, and try again.");
        //     return RedirectToAction(nameof(Error), error);
        // }
        catch (Exception exception)
        {
            var error = new ErrorViewModel("C-13C - Internal server error.");
            error.Errors.Add(exception.Message);
            return RedirectToAction(nameof(Error), error);
        }
    }

    public async Task<IActionResult> Delete([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null)
        {
            var error = new ErrorViewModel("C-14C - Customer Identification can not be null.");
            return RedirectToAction(nameof(Error), error);
        }

        try
        {
            return View(await new DeleteCustomerService().GetById(context, id));
        }
        catch (Exception exception)
        {
            var error = new ErrorViewModel("C-16C - Internal server error.");
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
            var error = new ErrorViewModel(ModelState.GetErrors("C-17C - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }
        try
        {
            Ok(await new DeleteCustomerService().Delete(context, id));
            return RedirectToAction(nameof(Index));
        }
        // catch (DbUpdateException)
        // {
        //     var error = new ErrorViewModel("C-19C - Can not delete this Customer.");
        //     return RedirectToAction(nameof(Error), error);
        // }
        catch (Exception exception)
        {
            var error = new ErrorViewModel("C-20C - Internal server error.");
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