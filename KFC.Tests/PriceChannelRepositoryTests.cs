using Xunit;
using KFC.Gateways.SQLite;
using KFC.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging.Abstractions;

public class PriceChannelRepositoryTests
{
    private ApplicationContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_PriceChannel")
            .Options;
        return new ApplicationContext(options);
    }

    [Fact]
    public async Task CreateAsync_AddsPriceChannelToDatabase()
    {
        // Arrange
        var context = GetInMemoryContext();
        var product = new Product { Name = "Pollo Broaster", Description = "Clásico pollo frito" };
        var channel = new Channel { Name = "RAPPI" };
        context.Product.Add(product);
        context.Channel.Add(channel);
        await context.SaveChangesAsync();

        var repo = new KFC.Gateways.SQLite.PriceChannelRepository(context, NullLogger<KFC.Gateways.SQLite.PriceChannelRepository>.Instance);
        var priceChannel = new PriceChannel { ProductId = product.ProductId, ChannelId = channel.ChannelId, Amount = 25.5m, IsActive = true };

        // Act
        await repo.CreateAsync(priceChannel);
        await context.SaveChangesAsync();

        // Assert
        Assert.Equal(1, context.PriceChannel.Count());
        Assert.Equal(25.5m, context.PriceChannel.First().Amount);
        Assert.Equal(product.ProductId, context.PriceChannel.First().ProductId);
        Assert.Equal(channel.ChannelId, context.PriceChannel.First().ChannelId);
    }
} 