using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace KFC.Gateways.SQLite;

public class ContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
	public ApplicationContext CreateDbContext(string[] args)
	{
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "KFC.WebApi");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Archivo de configuraci�n principal
            .Build();

        optionsBuilder.UseSqlite(configuration.GetConnectionString("DefaultConnection"));


        return new ApplicationContext(optionsBuilder.Options);
    }
}