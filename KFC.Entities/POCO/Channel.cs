namespace KFC.Entities;

/// <summary>
/// Representa un canal de venta donde se ofrece un producto.
/// </summary>
public class Channel :Base
{
    public Channel()
    {

    }
    public Channel(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Identificador único del canal.
    /// </summary>
    public int ChannelId { get; set; }

    /// <summary>
    /// Nombre identificador del canal (p.ej., "Online", "Tienda Física").
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Precios asociados a este canal para diferentes productos.
    /// </summary>
    public ICollection<PriceChannel> PriceChannels { get; set; } = new List<PriceChannel>();
}