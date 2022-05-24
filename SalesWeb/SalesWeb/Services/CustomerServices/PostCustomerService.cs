namespace SalesWeb.Services.CustomerServices;

public class PostCustomerService
{
    public async Task<PostCustomerViewModel> Post([FromServices] SalesWebDbContext context, PostCustomerViewModel model)
    {
        var customer = new Customer(Guid.NewGuid(), new Name(model.FirstName, model.LastName),
            new Email(model.Email, true), //confirmed will be false later
            new DocumentIdentificationNumber(model.DocumentIdentificationNumber),
            model.BirthDate);

        context.Add(customer);
        await context.SaveChangesAsync();
        return model;
    }
}