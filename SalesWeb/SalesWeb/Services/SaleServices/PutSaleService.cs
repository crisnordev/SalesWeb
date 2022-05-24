namespace SalesWeb.Services.SaleServices;

public class PutSaleService
{
    public async Task<PutSaleViewModel> GetById([FromServices] SalesWebDbContext context, Guid id)
    {
        var sale = await context.Sales.AsNoTracking()
            .Include(x => x.Seller)
            .Include(x => x.Customer)
            .Include(x => x.SoldProducts)
            .FirstOrDefaultAsync(x => x.SaleId == id);

        var putSaleViewModel = new PutSaleViewModel(sale.SaleId,sale.Customer.Name.ToString(), sale.Seller.Name.ToString(), 0,
            0, sale.TotalAmount);
        putSaleViewModel.SoldProducts = (IReadOnlyCollection<SoldProduct>)sale.SoldProducts;

        return putSaleViewModel;
    }

    public async Task<PutSaleViewModel> Put([FromServices] SalesWebDbContext context, Guid id, PutSaleViewModel model)
    {
        var sale = await context.Sales
            .Include(x => x.Seller)
            .Include(x => x.Customer)
            .Include(x => x.SoldProducts)
            .FirstOrDefaultAsync(x => x.SaleId == id);
        
        var product = await context.Products.FirstOrDefaultAsync(x => x.ProductId == model.ProductId);
        
        var soldProduct = new SoldProduct(Guid.NewGuid(), product.ProductId, new ProductName(
            product.ProductName.ProductFullName), model.ProductQuantity, product.Price);

        sale.TotalAmount += model.ProductQuantity * product.Price;
        sale.SoldProducts.Add(soldProduct);
        
        await context.SoldProducts.AddAsync(soldProduct);
        context.Sales.Update(sale);
        await context.SaveChangesAsync();

        return model;
    }
}