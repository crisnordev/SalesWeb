using SalesWeb.ViewModels.CustomerViewModels;

namespace SalesWeb.Services.CustomerServices;

public class PutCustomerService
{
    public async Task<PutCustomerViewModel> GetById([FromServices] SalesWebDbContext context, Guid id) =>
        await context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.CustomerId == id);

    public async Task<PutCustomerViewModel> Put([FromServices] SalesWebDbContext context, Guid id,
        PutCustomerViewModel model)
    {
        var customer = await context.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);
        customer.Name = new Name(model.FirstName, model.LastName);
        if(customer.Email.Address != model.Email)
            customer.Email = new Email(model.Email, true); //confirmed will be false later
        
        context.Update(customer);
        await context.SaveChangesAsync();
        return model;
    }
}