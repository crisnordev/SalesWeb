namespace SalesWeb.Data.Mappings;

public class SellerMap : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        builder.ToTable("Seller");
        
        builder.HasKey(x => x.SellerId);
        
        builder.OwnsOne(x => x.Name)
            .Property(x => x.FirstName)
            .HasColumnName("FirstName")
            .HasMaxLength(60)
            .IsRequired();
        
        builder.OwnsOne(x => x.Name)
            .Property(x => x.LastName)
            .HasColumnName("LastName")
            .HasMaxLength(120)
            .IsRequired();
        
        builder.OwnsOne(x => x.Name)
            .Property(x => x.CompleteName)
            .HasColumnName("CompleteName")
            .HasMaxLength(180)
            .IsRequired();

        builder.OwnsOne(x => x.Email)
            .Property(x => x.Address)
            .HasColumnName("Email")
            .HasColumnType("VARCHAR")
            .HasMaxLength(160)
            .IsRequired();

        builder.OwnsOne(x => x.Email)
            .Property(x => x.Confirmed)
            .HasColumnName("EmailConfirmed")
            .IsRequired();

        builder.OwnsOne(x => x.Email, nest =>
        {
            nest.OwnsOne(x => x.VerificationCode, code =>
            {
                code.Property(x => x.Code)
                    .HasColumnName("EmailVerificationCode")
                    .HasMaxLength(8)
                    .IsRequired();
                
                code.Property(x => x.ExpirationDate)
                    .HasColumnName("EmailExpirationDate")
                    .HasMaxLength(60);
                
                code.Property(x => x.Verified)
                    .HasColumnName("CodeVerified")
                    .IsRequired();
            });
        });
        
        builder.OwnsOne(x => x.DocumentIdentificationNumber)
            .Property(x => x.Number)
            .HasColumnName("DocumentIdentificationNumber")
            .HasColumnType("VARCHAR")
            .HasMaxLength(14)
            .IsRequired();

        builder.Property(x => x.Password)
            .HasColumnName("Password")
            .HasColumnType("VARCHAR")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.BirthDate)
            .HasColumnName("BirthDate")
            .HasColumnType("SMALLDATETIME")
            .HasMaxLength(60)
            .IsRequired();
    }
}