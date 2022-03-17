namespace SalesWeb.Controllers;

[Controller]
public class CustomerController : Controller
{
    public async  Task<IActionResult> Index([FromServices] SalesWebDbContext context)
    {
        
        var customers = await context.Customers.AsNoTracking().ToListAsync();
        return  View(customers);
    }
    
    
    public async Task<IActionResult> GetById([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null)
            return NotFound();

        var customer = await context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (customer == null)
            return NotFound();

        return View(customer);
    }

    public IActionResult Post()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Post([FromServices] SalesWebDbContext context, [Bind("Name,Email,Cpf,BirthDate")] Customer model)
    {
        if (ModelState.IsValid)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Email = model.Email,
                Cpf = model.Cpf,
                BirthDate = model.BirthDate
            };
            try
            {
                context.Add(customer);
                await context.SaveChangesAsync();
                
                View(customer);
                
                return RedirectToAction(nameof(Index));

            }
            catch (DbUpdateException)
            {
                return StatusCode(400, "This e-mail has already been used.");
            }
        }

        return View();
    }


    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null)
            return NotFound();
            
        var customer = await context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (customer == null)
            return NotFound();

        return View(customer);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, Guid id, [Bind("Name,Email,Cpf,BirthDate")] Customer customer)
    {
        if (id != customer.Id)
            return NotFound();


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
                return StatusCode(500, "Unable to edit Customer.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null)
            return NotFound();

        var customer = await context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (customer == null)
            return NotFound();

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
                return StatusCode(500, "Unable to delete Customer.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        return RedirectToAction(nameof(Index));
    }

}
