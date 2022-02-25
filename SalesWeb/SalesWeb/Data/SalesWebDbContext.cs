namespace SalesWeb.Data;

public class SalesWebDbContext : DbContext
{
    public DbSet<Seller>? Sellers { get; set; }
    public DbSet<Product>? Products { get; set; }
    public DbSet<Customer>? Customers { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("Server=localhost,1433;Database=SalesWeb;User ID=sa;Password=1q2w3e4r@#$");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().Property(x => x.Price).HasPrecision(18, 2);
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new SellerMap());
        modelBuilder.ApplyConfiguration(new ProductMap());
        modelBuilder.ApplyConfiguration(new CustomerMap());
    }

}