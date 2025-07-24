using System.Text.RegularExpressions;

using KFC.UseCases.DTOs;


namespace KFC.UseCases.Validator;

public class UserNameValidator
{
    private const int minLength = 5;
    private const int maxLength = 30; 

    public static List<MessageDto> Validate(string username)
    {
        var result = new List<MessageDto>();

        if (string.IsNullOrEmpty(username))
            result.Add(new MessageDto("El nombre de usuario no puede estar vacío."));

        // Verificar la longitud del nombre de usuario
        if (username.Length < minLength || username.Length > maxLength)
            result.Add(new MessageDto($"El nombre de usuario debe tener entre {minLength} y {maxLength} caracteres."));

        // Verificar si el nombre de usuario contiene solo caracteres alfanuméricos y guiones bajos
    
        if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$"))
            result.Add(new MessageDto("El nombre de usuario solo puede contener letras, números y guiones bajos."));

        // Verificar si el nombre de usuario comienza o termina con un guion bajo
        if (username.StartsWith("_") || username.EndsWith("_"))
            result.Add(new MessageDto("El nombre de usuario no puede comenzar ni terminar con un guion bajo."));

        // Verificar si el nombre de usuario contiene dos guiones bajos consecutivos
        if (username.Contains("__"))
            result.Add(new MessageDto("El nombre de usuario no puede contener dos guiones bajos consecutivos."));

        return result;

    }
}

