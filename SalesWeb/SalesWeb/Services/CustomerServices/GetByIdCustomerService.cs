namespace SalesWeb.Services.CustomerServices;

public class GetByIdCustomerService
{
    public async Task<GetCustomerViewModel> GetById([FromServices] SalesWebDbContext context, Guid id) =>
        await context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.CustomerId == id);
}