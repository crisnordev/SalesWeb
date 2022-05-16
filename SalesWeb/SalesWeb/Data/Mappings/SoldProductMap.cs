namespace SalesWeb.Data.Mappings;

public class SoldProductMap : IEntityTypeConfiguration<SoldProduct>
{
    public void Configure(EntityTypeBuilder<SoldProduct> builder)
    {
        builder.ToTable("SoldProduct");

        builder.HasKey(x => x.SoldProductId);
        
        builder.Property(x => x.ProductId)
            .HasColumnName("ProductId")
            .HasColumnType("SMALLINT")
            .IsRequired();
        
        builder.OwnsOne(x => x.ProductName)
            .Property(x => x.ProductFullName)
            .HasColumnName("ProductName")
            .HasColumnType("VARCHAR")
            .HasMaxLength(160)
            .IsRequired();
        
        builder.Property(x => x.Quantity)
            .HasColumnName("Quantity")
            .HasColumnType("INT")
            .IsRequired();
        
        builder.Property(x => x.Price)
            .HasColumnName("Price")
            .HasColumnType("DECIMAL(18, 2)")
            .IsRequired();
    }
}