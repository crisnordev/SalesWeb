using SalesWeb.ViewModels.ProductViewModels;

namespace SalesWeb.Services.ProductServices;

public class GetProductService
{
    public async Task<List<GetProductViewModel>> Get([FromServices] SalesWebDbContext context)
    {
        var productsContext = await context.Products.AsNoTracking().ToListAsync();
        var products = productsContext.Select(x => (GetProductViewModel) x).ToList();
        return products;
    }
}