using SalesWeb.ViewModels.ProductViewModels;

namespace SalesWeb.Services.ProductServices;

public class GetByIdProductService
{
    public async Task<GetProductViewModel> GetById([FromServices] SalesWebDbContext context, int id) =>
        await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == id);
}