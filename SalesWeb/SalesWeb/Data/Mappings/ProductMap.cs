using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesWeb.Data.Mappings;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");
        
        builder.HasKey(x => x.ProductId);
        
        builder.OwnsOne(x => x.ProductName)
            .Property(x => x.ProductFullName)
            .HasColumnName("ProductName")
            .HasColumnType("VARCHAR")
            .HasMaxLength(160)
            .IsRequired();
        
        builder.Property(x => x.Price)
            .HasColumnName("Price")
            .HasColumnType("DECIMAL(18,2)")
            .IsRequired();
    }
}