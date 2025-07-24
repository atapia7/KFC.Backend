using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using KFC.Entities;


namespace KFC.Gateways.SQLite;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(name: "Products", schema: "dbo");
        builder.HasKey(e => e.ProductId);

        builder.Property(e => e.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.HasMany(e => e.PriceChannels)
            .WithOne(pc => pc.Product)
            .HasForeignKey(pc => pc.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(prop => prop.CreatedAt)
            .IsRequired(required: true);
        builder.Property(prop => prop.UpdatedAt)
            .IsRequired(required: true);
    }
}

