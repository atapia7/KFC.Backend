using System.Text.RegularExpressions;

using KFC.UseCases.DTOs;

namespace KFC.UseCases.Validator;

public class PasswordValidator
{
    private const int minLength = 5;
    private const int maxLength = 30;

    public static List<MessageDto> Validate(string password)
    {
        string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{" + minLength + "," + maxLength + "}$";
        var result = new List<MessageDto>();

        try
        {

            if (!Regex.IsMatch(password, pattern))
                result.Add(
                    new MessageDto($"La contraseña no cumple con los requisitos mínimos. Debe contener al menos 1 " +
                                      $"letra minúscula, 1 letra mKFCúscula, 1 número, 1 carácter especial y tener una longitud " +
                                      $"de {minLength} a {maxLength} caracteres"));

            return result;
        }
        catch (Exception ex)
        {
            return new List<MessageDto> {
                new MessageDto(String.Format("Ocurrio un error en el momento de la validación del PASSWORD: {0}", ex.Message))
            };
        }
    }
}
