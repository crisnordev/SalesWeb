using SalesWeb.ViewModels.ProductViewModels;
using SalesWeb.ViewModels.SellerViewModels;

namespace SalesWeb.Services.SellerServices;

public class PutSellerService
{
    public async Task<PutSellerViewModel> GetById([FromServices] SalesWebDbContext context, Guid id) =>
        await context.Sellers.AsNoTracking().FirstOrDefaultAsync(x => x.SellerId == id);

    public async Task<PutSellerViewModel> Put([FromServices] SalesWebDbContext context, Guid id,
        PutSellerViewModel model)
    {
        var seller = await context.Sellers.FirstOrDefaultAsync(x => x.SellerId == id);
        seller.Name = new Name(model.FirstName, model.LastName);
        seller.Email = new Email(model.Email, true);
        seller.Password = model.Password;

        context.Update(seller);
        await context.SaveChangesAsync();
        return model;
    }
    
}