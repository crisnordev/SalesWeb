namespace SalesWeb.Controllers;

[Controller]
public class ProductController : Controller
{
    public async  Task<IActionResult> Index([FromServices] SalesWebDbContext context)
    {
        
        List<Product> products = await context.Products.AsNoTracking().ToListAsync();
        return  View(products);
    }
    
    
    public async Task<IActionResult> GetById([FromServices] SalesWebDbContext context, int id)
    {
        if (id == null)
            return NotFound();

        var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (product == null)
            return NotFound();

        return View(product);
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromServices] SalesWebDbContext context,[Bind("Id,Name,Price")] Product product)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (product == null)
                    return NotFound();

                context.Add(product);
                await context.SaveChangesAsync();
                
                View(product);
                
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
            
        var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (product == null)
            return NotFound();

        return View(product);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update([FromServices] SalesWebDbContext context, int id, [Bind("Id,Name,Price")] Product product)
    {
        if (id != product.Id)
            return NotFound();


        if (ModelState.IsValid)
        {
            try
            {
                context.Update(product);
                await context.SaveChangesAsync();
                Ok(product);
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

        var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (product == null)
            return NotFound();

        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed([FromServices] SalesWebDbContext context, int id)
    {
        
        var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (ModelState.IsValid)
        {
            try
            {
                context.Products.Remove(product!);
                await context.SaveChangesAsync();
                Ok(product);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Unable to delete product.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        return RedirectToAction(nameof(Index));
    }


}