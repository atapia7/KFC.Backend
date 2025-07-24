using KFC.UseCases.DTOs;

namespace KFC.UseCases.Validator;

public class PlateValidator
{
    private const int Length = 6;

    public static List<MessageDto> Validate(string plate)
    {
        var result = new List<MessageDto>();

        if (string.IsNullOrEmpty(plate))
            result.Add(new MessageDto("El numero de placa no puede estar vacío."));

        // Verificar la longitud del nombre de usuario
        if (plate.Length != Length)
            result.Add(new MessageDto($"El numero de placa debe tener 6 caracteres."));

        return result;

    }
}

