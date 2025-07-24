using KFC.UseCases.DTOs.Input;

namespace KFC.UseCases.DTOs.Input;

public class CreateChannelDto : JwtAuthorization
{
    /// <summary>
    /// Descripcion breve del nombre
    /// </summary>           
    public string Name { get; set; }

}
