namespace KFC.UseCases.DTOs.Input;

public class CreateAccountDto: JwtAuthorization
{
    public string UserName { get; set; }

    /// <summary>Correo electrónico del usuario.</summary>
    public string Email { get; set; }

    /// <summary>Contraseña en texto plano; se hasheará al guardar.</summary>
    public string Password { get; set; }


}
