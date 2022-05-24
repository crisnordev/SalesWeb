namespace SalesWeb.Services.SaleServices;

public class PostSaleService
{
    public async Task<PostSaleViewModel> Post([FromServices] SalesWebDbContext context, PostSaleViewModel model)
    {
        var customer = await context.Customers.FirstOrDefaultAsync(x => x.CustomerId == model.CustomerId);
        var seller = await context.Sellers.FirstOrDefaultAsync(x => x.SellerId == model.SellerId);
        var product = await context.Products.FirstOrDefaultAsync(x => x.ProductId == model.ProductId);

        var sale = new Sale
        {
            SaleId = Guid.NewGuid(),
            Customer = customer,
            Seller = seller,
            TotalAmount = model.ProductQuantity * product.Price
        };
        model.Id = sale.SaleId;

        sale.SoldProducts = new List<SoldProduct>
        {
            new(Guid.NewGuid(), product.ProductId, new ProductName(product.ProductName.ProductFullName),
                model.ProductQuantity, product.Price)
        };
        
        await context.SoldProducts.AddRangeAsync(sale.SoldProducts);
        await context.Sales.AddAsync(sale);
        await context.SaveChangesAsync();

        return model;
    }

}