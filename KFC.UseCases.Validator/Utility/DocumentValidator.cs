using KFC.Entities.Utility;
using KFC.UseCases.DTOs;


namespace KFC.UseCases.Validator;

public class DocumentValidator { 


    public static List<MessageDto> Validate(string documentType, string documentNumber)
    {
        var result = new List<MessageDto>();

        if (string.IsNullOrEmpty(documentNumber))
            result.Add(new MessageDto("Debe ingresar el numero de documento"));

        return result;
    }



}
