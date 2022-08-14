using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesWeb.Data.Mappings;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name).
            IsRequired().
            HasColumnName("Name").
            HasColumnType("NVARCHAR").
            HasMaxLength(160);
        
        builder.Property(x => x.Price).
            IsRequired().
            HasColumnName("Price")
            .HasColumnType("DECIMAL(18,2)");
    }
}