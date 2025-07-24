using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFC.UseCases.DTOs.Output;

public class ProductDto
{
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

    /// <summary>
    /// Crea una nueva instancia de ProductDto.
    /// </summary>
    public ProductDto() { }

    /// <summary>
    /// Inicializa una nueva instancia de ProductDto con valores.
    /// </summary>
    public ProductDto(int productId, string name, string description)
    {
        ProductId = productId;
        Name = name;
        Description = description;
    }
}
