namespace KFC.Entities;

/// <summary>
/// Representa un producto disponible en el sistema.
/// </summary>
public class Product : Base
{
    public Product() { }

    public Product(string name, string description, Account owner)
    {
        Name = name;
        Description = description;
        Owner = owner;
    }

    /// <summary>
    /// Identificador único del producto.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Nombre descriptivo del producto.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Descripción detallada del producto.
    /// </summary>
    public string Description { get; set; }

    // Clave foránea al dueño/propietario de este producto:
    public int? OwnerId { get; set; }

    // Propiedad de navegación al Account que “posee” este producto
    public Account Owner { get; set; }

    /// <summary>
    /// Precios asociados al producto por canal.
    /// </summary>
    public ICollection<PriceChannel> PriceChannels { get; set; } = new List<PriceChannel>();
}