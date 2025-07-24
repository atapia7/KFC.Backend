using System.ComponentModel;

namespace KFC.UseCases.DTOs.Input;

public class LoginDto
{
    public string UserName { get; set; }

    [DefaultValue("Passw0rd$")]
    public string Password { get; set; }

}