using Xunit;
using KFC.Gateways.SQLite;
using KFC.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging.Abstractions;

public class ProductRepositoryTests
{
    private ApplicationContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        return new ApplicationContext(options);
    }

    [Fact]
    public async Task CreateAsync_AddsProductToDatabase()
    {
        // Arrange
        var context = GetInMemoryContext();
        var repo = new KFC.Gateways.SQLite.ProductRepository(context, NullLogger<KFC.Gateways.SQLite.ProductRepository>.Instance);
        var product = new Product { Name = "Pollo Broaster", Description = "Clásico pollo frito" };

        // Act
        await repo.CreateAsync(product);
        await context.SaveChangesAsync();

        // Assert
        Assert.Equal(1, context.Product.Count());
        Assert.Equal("Pollo Broaster", context.Product.First().Name);
    }
}
