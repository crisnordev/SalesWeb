namespace SalesWeb.Data.Mappings;

public class SaleMap : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sale");

        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("CustomerId")
            .HasConstraintName("FK_Sale_CustomerID")
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(x => x.Seller)
            .WithMany()
            .HasForeignKey("SellerId")
            .HasConstraintName("FK_Sale_SellerID")
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(x => x.TotalAmount)
            .IsRequired()
            .HasColumnName("TotalAmount")
            .HasColumnType("DECIMAL(18,2)");
        
        builder.HasMany(x => x.SoldProducts)
            .WithMany(x => x.Sales)
            .UsingEntity<Dictionary<string, object>>(
                "SaleSoldProduct",
                sale => sale
                    .HasOne<SoldProduct>()
                    .WithMany()
                    .HasForeignKey("SoldProductId")
                    .HasConstraintName("FK_SaleSoldProduct_Sale_SoldProductId")
                    .OnDelete(DeleteBehavior.Cascade),
                soldProduct => soldProduct
                    .HasOne<Sale>()
                    .WithMany()
                    .HasForeignKey("SaleId")
                    .HasConstraintName("FK_SaleSoldProduct_SoldProduct_SaleId")
                    .OnDelete(DeleteBehavior.Cascade));
    }
}