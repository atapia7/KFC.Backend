using Xunit;
using KFC.Gateways.SQLite;
using KFC.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging.Abstractions;

public class ChannelRepositoryTests
{
    private ApplicationContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_Channel")
            .Options;
        return new ApplicationContext(options);
    }

    [Fact]
    public async Task CreateAsync_AddsChannelToDatabase()
    {
        // Arrange
        var context = GetInMemoryContext();
        var repo = new KFC.Gateways.SQLite.ChannelRepository(context, NullLogger<KFC.Gateways.SQLite.ChannelRepository>.Instance);
        var channel = new Channel { Name = "RAPPI" };

        // Act
        await repo.CreateAsync(channel);
        await context.SaveChangesAsync();

        // Assert
        Assert.Equal(1, context.Channel.Count());
        Assert.Equal("RAPPI", context.Channel.First().Name);
    }
} 