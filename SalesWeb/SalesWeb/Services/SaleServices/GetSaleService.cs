namespace SalesWeb.Services.SaleServices;

public class GetSaleService
{
    public async Task<List<GetSaleViewModel>> Get([FromServices] SalesWebDbContext context)
    {
        var salesContext = await context.Sales.AsNoTracking()
            .Include(x => x.Seller)
            .Include(x => x.Customer)
            .ToListAsync();
        var sales = salesContext.Select(item => new GetSaleViewModel(
                item.SaleId, item.Customer.Name.ToString(), item.Seller.Name.ToString(), item.TotalAmount))
            .ToList();
        return sales;
    }
}