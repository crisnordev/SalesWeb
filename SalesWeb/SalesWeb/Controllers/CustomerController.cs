using SalesWeb.ViewModels.CustomerViewModels;

namespace SalesWeb.Controllers;

[Controller]
public class CustomerController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index([FromServices] SalesWebDbContext context)
    {
        try
        {
            var customersContext = await context.Customers.AsNoTracking().ToListAsync();
            var customers = customersContext.Select(x => (GetCustomerViewModel) x).ToList();
            return View(customers);
        }
        catch
        {
            var error = new ErrorViewModel("C-01C - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetById([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null)
        {
            var error = new ErrorViewModel("C-02C - Customer Identification can not be null.");
            return RedirectToAction(nameof(Error), error);
        }

        try
        {
            GetCustomerViewModel customer = await context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (customer != null) return View(customer);
            var error = new ErrorViewModel("C-03C - Can not find Customer.");
            return RedirectToAction(nameof(Error), error);
        }
        catch
        {
            var error = new ErrorViewModel("C-04C - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }

    public IActionResult Post()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Post([FromServices] SalesWebDbContext context, EditorCustomerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-05C - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }

        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            DocumentId = model.DocumentId,
            BirthDate = model.BirthDate
        };
        try
        {
            context.Add(customer);
            await context.SaveChangesAsync();

            View(model);
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            var error = new ErrorViewModel("C-06C - This e-mail has already been used.");
            return RedirectToAction(nameof(Error), error);
        }
        catch
        {
            var error = new ErrorViewModel("C-07C - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }


    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null)
        {
            var error = new ErrorViewModel("C-08C - Customer Identification can not be null.");
            return RedirectToAction(nameof(Error), error);
        }

        try
        {
            EditorCustomerViewModel customer = await context.Customers.AsNoTracking().FirstOrDefaultAsync(x => 
                x.Id == id);
            return View(customer);
        }
        catch
        {
            var errorView = new ErrorViewModel("C-09C - Can not find Customer.");
            return RedirectToAction(nameof(Error), errorView);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, Guid id,
        EditorCustomerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-10C - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }

        var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == id);
        if (customer == null)
        {
            var error = new ErrorViewModel("C-11C - Can not find Customer.");
            return RedirectToAction(nameof(Error), error);
        }

        customer.FirstName = model.FirstName;
        customer.LastName = model.LastName;
        customer.Email = model.Email;
        customer.DocumentId = model.DocumentId;
        customer.BirthDate = model.BirthDate;
        try
        {
            context.Update(customer);
            await context.SaveChangesAsync();

            Ok(customer);
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            var error = new ErrorViewModel("C-12C - Unable to update this Customer, check data, and try again.");
            return RedirectToAction(nameof(Error), error);
        }
        catch
        {
            var error = new ErrorViewModel("C-13C - Internal server error.");
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
            GetCustomerViewModel customer = await context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (customer != null) return View(customer);
            var error = new ErrorViewModel("C-15C - Can not find Customer.");
            return RedirectToAction(nameof(Error), error);
        }
        catch
        {
            var error = new ErrorViewModel("C-16C - Internal server error.");
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

        var customer = await context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (customer == null)
        {
            var error = new ErrorViewModel("C-18C - Can not find Customer.");
            return RedirectToAction(nameof(Error), error);
        }

        try
        {
            context.Customers.Remove(customer!);
            await context.SaveChangesAsync();

            Ok(customer);
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            var error = new ErrorViewModel("C-19C - Can not delete this Customer.");
            return RedirectToAction(nameof(Error), error);
        }
        catch
        {
            var error = new ErrorViewModel("C-20C - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(ErrorViewModel error) => View(new ErrorViewModel(error.Errors));
}