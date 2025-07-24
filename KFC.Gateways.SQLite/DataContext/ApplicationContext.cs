using KFC.Entities;
using Microsoft.EntityFrameworkCore;

namespace KFC.Gateways.SQLite;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AccountConfig());
        modelBuilder.ApplyConfiguration(new ChannelConfig());
        modelBuilder.ApplyConfiguration(new PriceChannelConfig());
        modelBuilder.ApplyConfiguration(new ProductConfig());

    }
     
    public DbSet<Account> Account { get; set; }
    public DbSet<Channel> Channel { get; set; }
    public DbSet<PriceChannel> PriceChannel { get; set; }
    public DbSet<Product> Product{ get; set; }


    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        UdpateChangeDatesAsync();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }


    private async Task UdpateChangeDatesAsync()
    {
        var entities = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        var dateNow = DateTime.Now.ToUniversalTime();;

        foreach (var item in entities)
        {
            if (item.Entity is Base entity)
            {
                if (item.State == EntityState.Added) entity.CreatedAt = dateNow;
                entity.UpdatedAt = dateNow.ToUniversalTime();;
            }
        }

        await Task.CompletedTask; // Marca el método como completado de manera asincrónica
    }

}

