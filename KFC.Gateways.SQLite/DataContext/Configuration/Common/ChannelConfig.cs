using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using KFC.Entities;


namespace KFC.Gateways.SQLite;

public class ChannelConfig : IEntityTypeConfiguration<Channel>
{
    public void Configure(EntityTypeBuilder<Channel> builder)
    {
        builder.ToTable(name: "Channels", schema: "dbo");
        builder.HasKey(e => e.ChannelId);

        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(prop => prop.CreatedAt)
          .IsRequired(required: true);
        builder.Property(prop => prop.UpdatedAt)
            .IsRequired(required: true);

        builder.HasMany(e => e.PriceChannels)
            .WithOne(pc => pc.Channel)
            .HasForeignKey(pc => pc.ChannelId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}