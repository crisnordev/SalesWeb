using SalesWeb.ViewModels.CustomerViewModels;

namespace SalesWeb.Controllers;

[Controller]
public class CustomerController : Controller
{
    public async  Task<IActionResult> Index([FromServices] SalesWebDbContext context)
    {
        var customersContext = await context.Customers.AsNoTracking().ToListAsync();
        var customers = customersContext.Select(x => (GetCustomerViewModel) x).ToList();
        return  View(customers);
    }
    
    
    public async Task<IActionResult> GetById([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null) return NotFound();
        var customer = new GetCustomerViewModel();
        customer = await context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (customer == null) return NotFound();

        return View(customer);
    }

    public IActionResult Post()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Post([FromServices] SalesWebDbContext context, EditorCustomerViewModel model)
    {
        if (ModelState.IsValid)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
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
                return StatusCode(400, "C-01C - This e-mail has already been used.");
            }
        }
        return View();
    }


    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null) return NotFound();

        var customer = new EditorCustomerViewModel();
        customer = await context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (customer == null) return NotFound();

        return View(customer);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, Guid id,
        EditorCustomerViewModel model)
    {
        var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == id);
        if (false | id != customer.Id) return NotFound();
        customer = model;
        
        if (ModelState.IsValid)
        {
            try
            {
                context.Update(customer);
                await context.SaveChangesAsync();
                
                Ok(customer);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "C-02C - Unable to edit Customer.");
            }
            catch (Exception)
            {
                return StatusCode(500, "C-03C - Internal server error.");
            }
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null) return NotFound();

        var customer = await context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (customer == null) return NotFound();

        return View(customer);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed([FromServices] SalesWebDbContext context, Guid id)
    {
        var customer = await context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (ModelState.IsValid)
        {
            try
            {
                context.Customers.Remove(customer!);
                await context.SaveChangesAsync();
                
                Ok(customer);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "C-04C - Unable to delete Customer.");
            }
            catch (Exception)
            {
                return StatusCode(500, "C-05C - Internal server error.");
            }
        }
        return RedirectToAction(nameof(Index));
    }
}
