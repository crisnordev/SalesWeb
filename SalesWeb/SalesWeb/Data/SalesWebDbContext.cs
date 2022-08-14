namespace SalesWeb.Data;

public class SalesWebDbContext : DbContext
{
    public SalesWebDbContext (DbContextOptions<SalesWebDbContext> options) : base(options) { }
    
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SoldProduct> SoldProducts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SellerMap());
        modelBuilder.ApplyConfiguration(new ProductMap());
        modelBuilder.ApplyConfiguration(new CustomerMap());
        modelBuilder.ApplyConfiguration(new SaleMap());
        modelBuilder.ApplyConfiguration(new SoldProductMap());
        modelBuilder.Entity<Product>().Property(x => x.Price).HasPrecision(18, 2);
        modelBuilder.Entity<SoldProduct>().Property(x => x.Price).HasPrecision(18, 2);
        modelBuilder.Entity<Sale>().Property(x => x.TotalAmount).HasPrecision(18, 2);
        base.OnModelCreating(modelBuilder);
    }
}