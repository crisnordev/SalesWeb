namespace SalesWeb.Controllers;

[Controller]
public class ProductController : Controller
{
    public async  Task<IActionResult> Index([FromServices] SalesWebDbContext context)
    {
        
        List<Product> products = await context.Products.AsNoTracking().ToListAsync();
        return  View(products);
    }
    
    
    public async Task<IActionResult> GetById([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null)
            return NotFound();

        var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (product == null)
            return NotFound();

        return View(product);
    }

    public IActionResult Post()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Post([FromServices] SalesWebDbContext context,[Bind("Name,Price")] Product model)
    {
        if (ModelState.IsValid)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Price = model.Price
            };
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
                return StatusCode(400, "C-01P - An issue has happen. Check information, and try again.");
            }
        }

        return View();
    }
    
    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, Guid id)
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
    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, Guid id, [Bind("Name,Price")] Product product)
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
                return StatusCode(500, "C-02P - Unable to edit product.");
            }
            catch (Exception)
            {
                return StatusCode(500, "C-03P - Internal server error.");
            }
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete([FromServices] SalesWebDbContext context, Guid id)
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
    public async Task<IActionResult> DeleteConfirmed([FromServices] SalesWebDbContext context, Guid id)
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
                return StatusCode(500, "C-03P - Unable to delete product.");
            }
            catch (Exception)
            {
                return StatusCode(500, "C-04P - Internal server error.");
            }
        }

        return RedirectToAction(nameof(Index));
    }


}