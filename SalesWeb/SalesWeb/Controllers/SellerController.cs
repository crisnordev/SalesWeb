namespace SalesWeb.Controllers;

[Controller]
public class SellerController : Controller
{
    public async  Task<IActionResult> Index([FromServices] SalesWebDbContext context)
    {
        
        List<Seller> sellers = await context.Sellers.AsNoTracking().ToListAsync();
        return  View(sellers);
    }
    
    
    public async Task<IActionResult> GetById([FromServices] SalesWebDbContext context, int id)
    {
        if (id == null)
            return NotFound();

        var seller = await context.Sellers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (seller == null)
            return NotFound();

        return View(seller);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromServices] SalesWebDbContext context, [Bind("Name,Email,Password,Cpf,BirthDate")] Seller seller)
    {
        if (ModelState.IsValid)
        {
            try
            {
                context.Add(seller);
                await context.SaveChangesAsync();
                
                View(seller);
                
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
            
        var seller = await context.Sellers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (seller == null)
            return NotFound();

        return View(seller);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update([FromServices] SalesWebDbContext context, int id, [Bind("Id,Name,Email,Cpf,Password,BirthDate")] Seller seller)
    {
        if (id != seller.Id)
            return NotFound();


        if (ModelState.IsValid)
        {
            try
            {
                context.Update(seller);
                await context.SaveChangesAsync();
                Ok(seller);
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

        var seller = await context.Sellers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (seller == null)
            return NotFound();

        return View(seller);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed([FromServices] SalesWebDbContext context, int id)
    {
        
        var seller = await context.Sellers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (ModelState.IsValid)
        {
            try
            {
                context.Sellers.Remove(seller!);
                await context.SaveChangesAsync();
                Ok(seller);
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