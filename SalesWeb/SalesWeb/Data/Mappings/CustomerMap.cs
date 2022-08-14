namespace SalesWeb.Data.Mappings;

public class CustomerMap : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.FirstName).
            IsRequired().
            HasColumnName("FirstName").
            HasColumnType("NVARCHAR").
            HasMaxLength(80);
        
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
        
        builder.Property(x => x.BirthDate).
            IsRequired().
            HasColumnName("BirthDate").
            HasColumnType("SMALLDATETIME").
            HasMaxLength(60).
            HasDefaultValueSql("GETDATE()");
    }
}