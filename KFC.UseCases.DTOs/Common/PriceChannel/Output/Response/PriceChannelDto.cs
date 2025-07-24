using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFC.UseCases.DTOs.Output;

public class PriceChannelDto
{
    /// <summary>
    /// Identificador único de la relación precio-canal.
    /// </summary>
    public int PriceChannelId { get; set; }

    /// <summary>
    /// Identificador del producto asociado.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Identificador del canal asociado.
    /// </summary>
    public int ChannelId { get; set; }

    /// <summary>
    /// Monto del precio.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Indica si el precio está activo.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Nombre del canal.
    /// </summary>
    public string ChannelName { get; set; }

    /// <summary>
    /// Nombre del producto.
    /// </summary>
    public string ProductName { get; set; }

    public PriceChannelDto() { }

    public PriceChannelDto(int priceChannelId, int productId, int channelId, decimal amount, bool isActive, string channelName, string productName)
    {
        PriceChannelId = priceChannelId;
        ProductId = productId;
        ChannelId = channelId;
        Amount = amount;
        IsActive = isActive;
        ChannelName = channelName;
        ProductName = productName;
    }
}
