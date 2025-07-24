using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using KFC.Entities;


namespace KFC.Gateways.SQLite;
public class PriceChannelConfig : IEntityTypeConfiguration<PriceChannel>
{
    public void Configure(EntityTypeBuilder<PriceChannel> builder)
    {
        builder.ToTable(name: "PriceChannels", schema: "dbo");
        builder.HasKey(e => e.PriceChannelId);

        builder.Property(e => e.Amount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(e => e.IsActive)
            .IsRequired();

        builder.HasIndex(e => new { e.ProductId, e.ChannelId })
            .IsUnique();

        builder.Property(prop => prop.CreatedAt)
            .IsRequired(required: true);
        builder.Property(prop => prop.UpdatedAt)
            .IsRequired(required: true);
    }
}