using SalesWeb.ViewModels.SellerViewModels;

namespace SalesWeb.Controllers;

[Controller]
public class SellerController : Controller
{
    public async  Task<IActionResult> Index([FromServices] SalesWebDbContext context)
    {
        
        var sellersContext = await context.Sellers.AsNoTracking().ToListAsync();
        var sellers = sellersContext.Select(x => (GetSellerViewModel) x).ToList();
        return  View(sellers);
    }

    public async Task<IActionResult> GetById([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null) return NotFound();

        var seller = new GetSellerViewModel();
        seller = await context.Sellers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (seller == null) return NotFound();

        return View(seller);
    }

    public IActionResult Post()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Post([FromServices] SalesWebDbContext context, EditorSellerViewModel model)
    {
        if (ModelState.IsValid)
        {
            var seller = new Seller
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Email = model.Email,
                DocumentId = model.DocumentId,
                Password = model.Password,
                BirthDate = model.BirthDate
            };
            try
            {
                context.Add(seller);
                await context.SaveChangesAsync();
                
                View(model);
                return RedirectToAction(nameof(Index));

            }
            catch (DbUpdateException)
            {
                return StatusCode(400, "C-01SE - This e-mail has already been used.");
            }
        }

        return View();
    }


    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null) return NotFound();

        var seller = new EditorSellerViewModel();
        seller = await context.Sellers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (seller == null) return NotFound();

        return View(seller);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, Guid id,
        EditorSellerViewModel model)
    {
        var seller = await context.Sellers.FirstOrDefaultAsync(x => x.Id == id);
        if (seller == null) return NotFound();
        
        seller.Name = model.Name;
        seller.Email = model.Email;
        seller.DocumentId = model.DocumentId;
        seller.Password = model.Password;
        seller.BirthDate = model.BirthDate;

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
                return StatusCode(500, "C-02SE - Unable to update product.");
            }
            catch (Exception)
            {
                return StatusCode(500, "C-03SE - Internal server error.");
            }
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null) return NotFound();

        var seller = new GetSellerViewModel();
        seller = await context.Sellers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (seller == null) return NotFound();

        return View(seller);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed([FromServices] SalesWebDbContext context, Guid id)
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
                return StatusCode(500, "C-04SE - Unable to update product.");
            }
            catch (Exception)
            {
                return StatusCode(500, "C-05SE - Internal server error.");
            }
        }
        return RedirectToAction(nameof(Index));
    }
}