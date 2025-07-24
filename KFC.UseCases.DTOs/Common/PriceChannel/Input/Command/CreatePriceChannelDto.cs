using KFC.UseCases.DTOs.Input;

namespace KFC.UseCases.DTOs.Input;

public class CreatePriceChannelDto : JwtAuthorization
{


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

}
