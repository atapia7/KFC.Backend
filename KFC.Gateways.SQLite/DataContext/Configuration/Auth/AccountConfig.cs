using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using KFC.Entities;

namespace KFC.Gateways.SQLite;
public class AccountConfig : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable(name: "Account", schema: "dbo");
        builder.HasKey(e => e.AccountId);

        builder.Property(e => e.UserName)
            .HasMaxLength(120)
            .IsRequired();
        builder.HasIndex(e => e.UserName).IsUnique();

        builder.Property(e => e.Email)
            .HasMaxLength(120)
            .IsRequired();
        builder.HasIndex(e => e.Email).IsUnique();

        builder.Property(e => e.PasswordHash)
            .HasMaxLength(255)
            .IsRequired(false);
        
        builder.Property(e => e.Token)
            .IsRequired(false);
        
        builder.Property(e => e.ExpireToken)
            .IsRequired(false);

        builder.Property(e => e.AccountType)
            .HasConversion<int>()
            .IsRequired();

        builder.HasMany(e => e.Products)
            .WithOne(p => p.Owner) // assuming Product has Owner navigation
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(prop => prop.CreatedAt)
            .IsRequired(required: true);
        builder.Property(prop => prop.UpdatedAt)
            .IsRequired(required: true);
    }
}