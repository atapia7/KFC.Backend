using KFC.Entities.Utility;
using KFC.UseCases.DTOs;
using System.Globalization;


namespace KFC.UseCases.Validator;

public class DateValidator
{
    // Método para validar la cadena
    public static List<MessageDto> Validate(string date)
    {

        var result = new List<MessageDto>();

        // Definir el formato de fecha esperado
        string format = "dd/MM/yyyy";

        // Intentar analizar la cadena usando el formato especificado
        DateTime dateResult;
        bool isValid = DateTime.TryParseExact(
            date,
            format,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out dateResult
        );

        if ( !isValid )
            result.Add(new MessageDto("Debe ingresar una fecha con formato dd/MM/yyyy"));
        
        
        return result;
    }
}
