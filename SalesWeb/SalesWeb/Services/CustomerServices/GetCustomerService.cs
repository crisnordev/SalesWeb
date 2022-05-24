namespace SalesWeb.Services.CustomerServices;

public class GetCustomerService 
{
    public async Task<List<GetCustomerViewModel>> Get([FromServices] SalesWebDbContext context)
    {
        var customersContext = await context.Customers.AsNoTracking().ToListAsync();
        var customers = customersContext.Select(customer => (GetCustomerViewModel) customer).ToList();
        return customers;
    }
}