using SalesWeb.ViewModels.SellerViewModels;

namespace SalesWeb.Controllers;

[Controller]
public class SellerController : Controller
{
    public async Task<IActionResult> Index([FromServices] SalesWebDbContext context)
    {
        try
        {
            var sellersContext = await context.Sellers.AsNoTracking().ToListAsync();
            var sellers = sellersContext.Select(customer => (GetSellerViewModel) customer).ToList();
            
            return View(sellers);
        }
        catch (Exception exception)
        {
            var error = new ErrorViewModel("C-01SE - Internal server error.");
            error.Errors.Add(exception.Message);
            return RedirectToAction(nameof(Error), error);
        }
    }

    public async Task<IActionResult> GetById([FromServices] SalesWebDbContext context, Guid id)
    {
        if (id == null)
        {
            var error = new ErrorViewModel("C-02SE - Seller Identification can not be null.");
            return RedirectToAction(nameof(Error), error);
        }

        try
        {
            var sellerContext = await context.Sellers.AsNoTracking().FirstOrDefaultAsync(x => x.SellerId == id);
            if (sellerContext == null)
            {
                var error = new ErrorViewModel("C-03SE - Can not find Seller.");
                return RedirectToAction(nameof(Error), error);
            }
            var seller = new GetSellerViewModel
            {
                SellerId = sellerContext.SellerId,
                Name = sellerContext.Name,
                Email = sellerContext.Email,
                DocumentIdentificationNumber = sellerContext.DocumentIdentificationNumber,
                Password = sellerContext.Password,
                BirthDate = sellerContext.BirthDate
            };
            return View(seller);
        }
        catch (Exception exception)
        {
            var error = new ErrorViewModel("C-04SE - Internal server error.");
            error.Errors.Add(exception.Message);
            return RedirectToAction(nameof(Error), error);
        }
    }

    public IActionResult Post()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Post([FromServices] SalesWebDbContext context, PostSellerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-05SE - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }

        var seller = new Seller(Guid.NewGuid(), new Name(model.FirstName, model.LastName),
            new Email(model.Email, true), //confirmed will be false later
            new DocumentIdentificationNumber(model.DocumentIdentificationNumber), model.Password, model.BirthDate);

        try
        {
            context.Add(seller);
            await context.SaveChangesAsync();

            View(model);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception exception)
        {
            var error = new ErrorViewModel("C-07SE - Internal server error.");
            error.Errors.Add(exception.Message);
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
            PutSellerViewModel seller = await context.Sellers.AsNoTracking().FirstOrDefaultAsync(x => x.SellerId == id);
            return View(seller);
        }
        catch (Exception exception)
        {
            var errorView = new ErrorViewModel("C-09SE - Can not find Seller.");
            errorView.Errors.Add(exception.Message);
            return RedirectToAction(nameof(Error), errorView);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Put([FromServices] SalesWebDbContext context, PutSellerViewModel model, Guid id)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorViewModel(ModelState.GetErrors("C-10SE - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }
        
        var seller = await context.Sellers.FirstOrDefaultAsync(x => x.SellerId == id);
        // if (seller == null)
        // {
        //     var error = new ErrorViewModel("C-11SE - Can not find Seller.");
        //     return RedirectToAction(nameof(Error), error);
        // }

        seller.Name = new Name(model.FirstName, model.LastName);
        if (seller.Email.Address != model.Email)
            seller.Email = new Email(model.Email, true); //confirmed will be false later
        seller.Password = model.Password;

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
        catch (Exception exception)
        {
            var error = new ErrorViewModel("C-13SE - Internal server error.");
            error.Errors.Add(exception.Message);
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
            var sellerContext = await context.Sellers.AsNoTracking().FirstOrDefaultAsync(x => x.SellerId == id);
            if (sellerContext == null)
            {
                var error = new ErrorViewModel("C-03SE - Can not find Seller.");
                return RedirectToAction(nameof(Error), error);
            }
            var seller = new GetSellerViewModel
            {
                SellerId = sellerContext.SellerId,
                Name = sellerContext.Name,
                Email = sellerContext.Email,
                DocumentIdentificationNumber = sellerContext.DocumentIdentificationNumber,
                Password = sellerContext.Password,
                BirthDate = sellerContext.BirthDate
            };
            return View(seller);
        }
        catch (Exception exception)
        {
            var error = new ErrorViewModel("C-16SE - Internal server error.");
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
            var error = new ErrorViewModel(ModelState.GetErrors("C-17SE - Can not validate this model."));
            return RedirectToAction(nameof(Error), error);
        }

        var seller = await context.Sellers.AsNoTracking().FirstOrDefaultAsync(x => x.SellerId == id);
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
        catch (Exception exception)
        {
            var error = new ErrorViewModel("C-20SE - Internal server error.");
            error.Errors.Add(exception.Message);
            return RedirectToAction(nameof(Error), error);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(ErrorViewModel error)
    {
        return View(new ErrorViewModel
        {
            Errors = error.Errors,
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}