using KFC.UseCases.DTOs;

namespace KFC.UseCases.Validator;

public class PhoneNumberValidator { 
    private const string pattern = @"^\+(?:[0-9] ?){6,14}[0-9]$";

    public static List<MessageDto> Validate(string phoneNumber)
    {
        var result = new List<MessageDto>();

        //if (!Regex.IsMatch(phoneNumber, pattern))
        //    result.Add(new Message("El número de teléfono no es válido."));


        return result;
    }
}
