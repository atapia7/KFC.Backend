using KFC.UseCases.DTOs.Input;

namespace KFC.UseCases.DTOs.Input;

public class CreateProductDto : JwtAuthorization
{
    /// <summary>
    /// Nombre descriptivo del producto.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Descripción detallada del producto.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Monto del precio.
    /// </summary>
    public decimal Amount { get; set; }

    // Clave foránea al dueño/propietario de este producto:
    public int? OwnerId { get; set; } //AccountID

}
