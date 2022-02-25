namespace SalesWeb.Controllers;

[Controller]
public class CustomerController : Controller
{
    public async  Task<IActionResult> Index([FromServices] SalesWebDbContext context)
    {
        
        var customers = await context.Customers.AsNoTracking().ToListAsync();
        return  View(customers);
    }
    
    
    public async Task<IActionResult> GetById([FromServices] SalesWebDbContext context, int id)
    {
        if (id == null)
            return NotFound();

        var customer = await context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (customer == null)
            return NotFound();

        return View(customer);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromServices] SalesWebDbContext context, [Bind("Name,Email,Cpf,BirthDate")] Customer customer)
    {
        if (ModelState.IsValid)
        {
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


    public async Task<IActionResult> Update([FromServices] SalesWebDbContext context, int id)
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
    public async Task<IActionResult> Update([FromServices] SalesWebDbContext context, int id, [Bind("Id,Name,Email,Cpf,BirthDate")] Customer customer)
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
                return StatusCode(500, "Unable to update product.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete([FromServices] SalesWebDbContext context, int id)
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
    public async Task<IActionResult> DeleteConfirmed([FromServices] SalesWebDbContext context, int id)
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
                return StatusCode(500, "Unable to update product.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        return RedirectToAction(nameof(Index));
    }

}
