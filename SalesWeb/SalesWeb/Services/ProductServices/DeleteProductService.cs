using SalesWeb.ViewModels.ProductViewModels;

namespace SalesWeb.Services.ProductServices;

public class DeleteProductService
{
    public async Task<PutProductViewModel> GetById([FromServices] SalesWebDbContext context, int id) =>
        await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == id);

    public async Task<PutProductViewModel> Delete([FromServices] SalesWebDbContext context, int id)
    {
        var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == id);
        context.Products.Remove(product);
        await context.SaveChangesAsync();
        return product;
    }
}