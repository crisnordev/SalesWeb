namespace SalesWeb.Services.SellerServices;

public class GetByIdSellerService
{
    public async Task<GetSellerViewModel> GetById([FromServices] SalesWebDbContext context, Guid id) =>
        await context.Sellers.AsNoTracking().FirstOrDefaultAsync(x => x.SellerId == id);
}