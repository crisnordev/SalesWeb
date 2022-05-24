using Microsoft.AspNetCore.Mvc.Rendering;
using SalesWeb.Services.SaleServices;

namespace SalesWeb.Controllers;

[Controller]
public class SaleController : Controller
{
    public async Task<IActionResult> Index([FromServices] SalesWebDbContext context)
    {
        try
        {
            return View(await new GetSaleService().Get(context));
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
            return View(await new GetByIdSaleService().GetById(context, id));
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
        
        try
        {
            View(await new PostSaleService().Post(context, model));
            return RedirectToAction(nameof(GetById), new {id = model.Id});
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
            ViewData["ProductId"] = new SelectList(context.Products, "ProductId", "ProductName");
            
            return View(await new PutSaleService().GetById(context, id));
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
        
        try
        {
            View(await new PutSaleService().Put(context, id, model));
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

        try
        {
            return View(await new DeleteSaleService().GetById(context, id));
        }
        catch (Exception exception)
        {
            var error = new ErrorViewModel("C-21SA - Internal server error.");
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
            var error = new ErrorViewModel(ModelState.GetErrors("C-23SA - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }
        
        try
        {
            View(await new DeleteSaleService().Delete(context, id));
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