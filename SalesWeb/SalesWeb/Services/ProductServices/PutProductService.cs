namespace SalesWeb.Services.ProductServices;

public class PutProductService
{
    public async Task<PutProductViewModel> GetById([FromServices] SalesWebDbContext context, int id) =>
        await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == id);

    public async Task<PutProductViewModel> Put([FromServices] SalesWebDbContext context, int id,
        PutProductViewModel model)
    {
        var product = await context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
        product.ProductName = new ProductName(model.ProductName);
        product.Price = model.Price;
        
        context.Update(product);
        await context.SaveChangesAsync();
        return model;
    }
    
}