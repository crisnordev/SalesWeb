namespace SalesWeb.Services.SaleServices;

public class DeleteSaleService
{
    public async Task<PutSaleViewModel> GetById([FromServices] SalesWebDbContext context, Guid id)
    {
        var saleContext = await context.Sales.AsNoTracking()
            .Include(x => x.Seller)
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.SaleId == id);
        
        var sale = new PutSaleViewModel(saleContext.SaleId, saleContext.Customer.Name.ToString(), saleContext.Seller.Name.ToString(),
            0, 0, saleContext.TotalAmount);

        return sale;
    }

    public async Task<PutSaleViewModel> Delete([FromServices] SalesWebDbContext context, Guid id)
    {
        var sale = await context.Sales.AsNoTracking()
            .Include(x => x.Seller)
            .Include(x => x.Customer)
            .Include(x => x.SoldProducts)
            .FirstOrDefaultAsync(x => x.SaleId == id);

        context.Sales.Remove(sale);
        await context.SaveChangesAsync();

        return new PutSaleViewModel();
    }
}