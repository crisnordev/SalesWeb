using SalesWeb.ViewModels.SellerViewModels;

namespace SalesWeb.Services.SellerServices;

public class PostSellerService
{
    public async Task<PostSellerViewModel> Post([FromServices] SalesWebDbContext context, PostSellerViewModel model)
    {
        var seller = new Seller(Guid.NewGuid(), new Name(model.FirstName, model.LastName), new Email(model.Email, true), 
            new DocumentIdentificationNumber(model.DocumentIdentificationNumber), model.Password, model.BirthDate);
       context.Add(seller);
        await context.SaveChangesAsync();
        return model;
    }
}