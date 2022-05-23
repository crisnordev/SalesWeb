using SalesWeb.ViewModels.ProductViewModels;

namespace SalesWeb.Services.ProductServices;

public class PostProductService
{
    public async Task<PostProductViewModel> Post([FromServices] SalesWebDbContext context, PostProductViewModel model)
    {
        var product = new Product(0, new ProductName(model.ProductName), model.Price);
       context.Add(product);
        await context.SaveChangesAsync();
        return model;
    }
}