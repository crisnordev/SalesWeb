namespace SalesWeb.Data.Mappings;

public class SellerMap : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        builder.ToTable("Seller");
        
        builder.HasKey(x => x.SellerId);
        
        builder.Property(x => x.FirstName).
            IsRequired().
            HasColumnName("FirstName").
            HasColumnType("NVARCHAR").
            HasMaxLength(160);
                
        builder.Property(x => x.LastName).
            IsRequired().
            HasColumnName("LastName").
            HasColumnType("NVARCHAR").
            HasMaxLength(160);
        
        builder.Property(x => x.Email).
            IsRequired().
            HasColumnName("Email").
            HasColumnType("VARCHAR").
            HasMaxLength(160);
        
        builder.Property(x => x.DocumentId).
            IsRequired().
            HasColumnName("DocumentId").
            HasColumnType("VARCHAR").
            HasMaxLength(14);
        
        builder.Property(x => x.Password).
            IsRequired().
            HasColumnName("Password").
            HasColumnType("VARCHAR").
            HasMaxLength(255);
        
        builder.Property(x => x.BirthDate).
            IsRequired().
            HasColumnName("BirthDate").
            HasColumnType("SMALLDATETIME").
            HasMaxLength(60).
            HasDefaultValueSql("GETDATE()");
    }
}