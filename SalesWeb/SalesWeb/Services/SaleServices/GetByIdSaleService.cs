namespace SalesWeb.Services.SaleServices;

public class GetByIdSaleService
{
    public async Task<GetSaleViewModel> GetById([FromServices] SalesWebDbContext context, Guid id)
    {
        var saleContext = await context.Sales.AsNoTracking()
            .Include(x => x.Seller)
            .Include(x => x.Customer)
            .Include(x => x.SoldProducts)
            .FirstOrDefaultAsync(x => x.SaleId == id);
        //mus validate if saleContext Exists

        var sale = new GetSaleViewModel(saleContext.SaleId, saleContext.Customer.Name.ToString(),
            saleContext.Seller.Name.ToString(),
            saleContext.TotalAmount);
        foreach (var item in saleContext.SoldProducts)
            sale.SoldProducts.Add(new SoldProduct(item.SoldProductId, item.ProductId,
                new ProductName(item.ProductName.ProductFullName),
                item.Quantity, item.Price));

        return sale;
    }
}