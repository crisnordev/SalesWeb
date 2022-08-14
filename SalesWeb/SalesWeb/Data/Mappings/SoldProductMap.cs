namespace SalesWeb.Data.Mappings;

public class SoldProductMap : IEntityTypeConfiguration<SoldProduct>
{
    public void Configure(EntityTypeBuilder<SoldProduct> builder)
    {
        builder.ToTable("SoldProduct");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.ProductId)
            .IsRequired()
            .HasColumnName("ProductId")
            .HasColumnType("VARCHAR")
            .HasMaxLength(36);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
        
        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasColumnName("Quantity")
            .HasColumnType("INT");
        
        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnName("Price")
            .HasColumnType("DECIMAL(18, 2)");
    }
}