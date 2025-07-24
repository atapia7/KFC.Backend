using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IO;


namespace KFC.Gateways.SQLite;

public class ContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
	public ApplicationContext CreateDbContext(string[] args)
	{
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

        var root = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..")
        );
        // Ahora root apunta a la carpeta de la solución
        var webApiPath = Path.Combine(root, "KFC.WebApi");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(webApiPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var conn = configuration.GetConnectionString("DefaultConnection");
        Console.WriteLine($"[DesignTime] Usando cadena: {conn}");

        optionsBuilder.UseSqlite(conn);

        return new ApplicationContext(optionsBuilder.Options);
    }
}