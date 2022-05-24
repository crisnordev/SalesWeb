namespace SalesWeb.Services.SellerServices;

public class DeleteSellerService
{
    public async Task<GetSellerViewModel> GetById([FromServices] SalesWebDbContext context, Guid id) =>
        await context.Sellers.AsNoTracking().FirstOrDefaultAsync(x => x.SellerId == id);

    public async Task<GetSellerViewModel> Delete([FromServices] SalesWebDbContext context, Guid id)
    {
        var seller = await context.Sellers.AsNoTracking().FirstOrDefaultAsync(x => x.SellerId == id);
        context.Sellers.Remove(seller);
        await context.SaveChangesAsync();
        return seller;
    }
}