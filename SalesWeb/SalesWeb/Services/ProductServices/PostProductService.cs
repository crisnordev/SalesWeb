using SalesWeb.ViewModels.ProductViewModels;

namespace SalesWeb.Services.ProductServices;

public class PostProductService
{
    public async Task<PostPutProductViewModel> Post([FromServices] SalesWebDbContext context, PostPutProductViewModel model)
    {
        var product = new Product(0, new ProductName(model.ProductName), model.Price);
       context.Add(product);
        await context.SaveChangesAsync();
        return model;
    }
}