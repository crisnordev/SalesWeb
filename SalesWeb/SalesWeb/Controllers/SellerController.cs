using SalesWeb.ViewModels.SellerViewModels;

namespace SalesWeb.Controllers;

[Controller]
public class SellerController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index([FromServices] SalesWebDbContext context)
    {
        try
        {
            var sellersContext = await context.Sellers.AsNoTracking().ToListAsync();
            var sellers = sellersContext.Select(x => (GetSellerViewModel) x).ToList();
            return View(sellers);
        }
        catch
        {
            var error = new ErrorViewModel("C-01SE - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetById([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null)
        {
            var error = new ErrorViewModel("C-02SE - Seller Identification can not be null.");
            return RedirectToAction(nameof(Error), error);
        }

        try
        {
            GetSellerViewModel seller = await context.Sellers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (seller == null) return View(seller);
            var error = new ErrorViewModel("C-03SE - Can not find Seller.");
            return RedirectToAction(nameof(Error), error);
        }
        catch
        {
            var error = new ErrorViewModel("C-04SE - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }

    public IActionResult Post()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Post([FromServices] SalesWebDbContext context, EditorSellerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-05SE - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }

        var seller = new Seller
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
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
            var error = new ErrorViewModel("C-06SE - This e-mail has already been used.");
            return RedirectToAction(nameof(Error), error);
        }
        catch
        {
            var error = new ErrorViewModel("C-07SE - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }
    
    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null)
        {
            var error = new ErrorViewModel("C-08SE - Seller Identification can not be null.");
            return RedirectToAction(nameof(Error), error);
        }

        try
        {
            EditorSellerViewModel seller = await context.Sellers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return View(seller);
        }
        catch
        {
            var errorView = new ErrorViewModel("C-09SE - Can not find Seller.");
            return RedirectToAction(nameof(Error), errorView);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, Guid id,
        EditorSellerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-10SE - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }

        var seller = await context.Sellers.FirstOrDefaultAsync(x => x.Id == id);
        if (seller == null)
        {
            var error = new ErrorViewModel("C-11SE - Can not find Seller.");
            return RedirectToAction(nameof(Error), error);
        }

        seller.FirstName = model.FirstName;
        seller.LastName = model.LastName;
        seller.Email = model.Email;
        seller.DocumentId = model.DocumentId;
        seller.Password = model.Password;
        seller.BirthDate = model.BirthDate;
        try
        {
            context.Update(seller);
            await context.SaveChangesAsync();

            Ok(seller);
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            var error = new ErrorViewModel("C-12SE - Unable to update this Seller, check data, and try again.");
            return RedirectToAction(nameof(Error), error);
        }
        catch
        {
            var error = new ErrorViewModel("C-13SE - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }
    
    public async Task<IActionResult> Delete([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null)
        {
            var error = new ErrorViewModel("C-14SE - Seller Identification can not be null.");
            return RedirectToAction(nameof(Error), error);
        }

        try
        {
            GetSellerViewModel seller = await context.Sellers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (seller != null) return View(seller);
            var error = new ErrorViewModel("C-15SE - Can not find Seller.");
            return RedirectToAction(nameof(Error), error);
        }
        catch
        {
            var error = new ErrorViewModel("C-16SE - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed([FromServices] SalesWebDbContext context, Guid id)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-17SE - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }

        var seller = await context.Sellers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (seller == null)
        {
            var error = new ErrorViewModel("C-18SE - Can not find Seller.");
            return RedirectToAction(nameof(Error), error);
        }

        try
        {
            context.Sellers.Remove(seller!);
            await context.SaveChangesAsync();
            Ok(seller);
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            var error = new ErrorViewModel("C-19SE - Can not delete this Seller.");
            return RedirectToAction(nameof(Error), error);
        }
        catch
        {
            var error = new ErrorViewModel("C-20SE - Internal server error.");
            return RedirectToAction(nameof(Error), error);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(ErrorViewModel error) => View(new ErrorViewModel(error.Errors));
}