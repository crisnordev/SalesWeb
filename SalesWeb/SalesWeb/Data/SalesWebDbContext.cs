using Microsoft.EntityFrameworkCore;
using SalesWeb.Data.Mappings;
using SalesWeb.Models;

namespace SalesWeb.Data;

public class SalesWebDbContext : DbContext
{
    public DbSet<Seller>? Sellers { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("Server=localhost,1433;Database=SalesWeb;User ID=sa;Password=password");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SellerMap());
    }

}