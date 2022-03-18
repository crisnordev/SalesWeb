namespace SalesWeb.Data.Mappings;

public class CustomerMap : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).
            IsRequired().
            HasColumnName("Name").
            HasColumnType("NVARCHAR").
            HasMaxLength(160);
        builder.Property(x => x.Email).
            IsRequired().
            HasColumnName("Email").
            HasColumnType("VARCHAR").
            HasMaxLength(160);
        builder.Property(x => x.DocumentId).
            IsRequired().
            HasColumnName("Cpf").
            HasColumnType("VARCHAR").
            HasMaxLength(11);
        builder.Property(x => x.BirthDate).
            IsRequired().
            HasColumnName("BirthDate").
            HasColumnType("SMALLDATETIME").
            HasMaxLength(60).
            HasDefaultValueSql("GETDATE()");
    }
}