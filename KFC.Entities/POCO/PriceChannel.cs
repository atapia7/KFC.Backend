namespace KFC.Entities;

/// <summary>
/// Define el precio de un producto en un canal específico.
/// </summary>
public class PriceChannel : Base
{
    public PriceChannel() { }

    public PriceChannel(int productId, int channelId, decimal amount, bool isActive)
    {
        ProductId = productId;
        ChannelId = channelId;
        Amount = amount;
        IsActive = isActive;
    }

    /// <summary>
    /// Identificador único de la relación precio-canal.
    /// </summary>
    public int PriceChannelId { get; set; }

    /// <summary>
    /// Clave foránea al producto.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Clave foránea al canal.
    /// </summary>
    public int ChannelId { get; set; }

    /// <summary>
    /// Monto del precio en la moneda del sistema.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Indica si el precio está activo o no.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Navegación al producto.
    /// </summary>
    public Product Product { get; set; }

    /// <summary>
    /// Navegación al canal.
    /// </summary>
    public Channel Channel { get; set; }
}
