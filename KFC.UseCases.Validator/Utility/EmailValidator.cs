using System.Text.RegularExpressions;

using KFC.UseCases.DTOs;

namespace KFC.UseCases.Validator;

public class EmailValidator {

    private const string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    public static List<MessageDto> Validate(string email)
    {
        var result = new List<MessageDto>();

        if (!Regex.IsMatch(email, pattern))
            result.Add(new MessageDto("La dirección de correo electrónico no es válida."));


        return result;
    }
}
