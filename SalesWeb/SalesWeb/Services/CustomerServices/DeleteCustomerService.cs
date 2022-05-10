using SalesWeb.ViewModels.CustomerViewModels;

namespace SalesWeb.Services.CustomerServices;

public class DeleteCustomerService
{
    public async Task<GetCustomerViewModel> GetById([FromServices] SalesWebDbContext context, Guid id) =>
        await context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.CustomerId == id);

    public async Task<Customer> Delete([FromServices] SalesWebDbContext context, Guid id)
    {
        var customer = await context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.CustomerId == id);
        context.Customers.Remove(customer!);
        await context.SaveChangesAsync();
        return customer;
    }
}