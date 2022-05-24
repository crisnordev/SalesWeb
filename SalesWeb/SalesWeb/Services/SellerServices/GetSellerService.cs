using SalesWeb.ViewModels.SellerViewModels;

namespace SalesWeb.Services.SellerServices;

public class GetSellerService
{
    public async Task<List<GetSellerViewModel>> Get([FromServices] SalesWebDbContext context)
    {
        var sellerContext = await context.Sellers.AsNoTracking().ToListAsync();
        var sellers = sellerContext.Select(x => (GetSellerViewModel) x).ToList();
        return sellers;
    }
}