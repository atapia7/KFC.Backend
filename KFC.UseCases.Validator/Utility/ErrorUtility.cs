using KFC.UseCases.DTOs;

namespace KFC.UseCases.Validator;


public class ErrorUtility
{

    private static readonly Dictionary<string, string> Errors = new Dictionary<string, string>()
    {
        {"0001", "USUARIO NO ENCONTRADO"},
        {"0002", "USUARIO NO TIENE EL ESTADO ESPERADO PARA SU ACTIVACION"},
        {"0003", "DEBE INGRESAR CONTRASENA"},
        {"0004", "DEBE INGRESAR CONTRASENA DE VERIFICACION"},
        {"0005", "CONTRASENA NO COINCIDEN"},
        {"0006", "DEBE INGRESAR APELLIDO MATERNO"},
        {"0007", "DEBE INGRESAR APELLIDO PATERNO"},
        {"0008", "DEBE INGRESAR CIUDAD"},
        {"0009", "DEBE INGRESAR DIRECCION"},
        {"00010", "DEBE INGRESAR LA IMAGEN EN FORMATO BASE64"},
        {"00011", "NO SE PUEDE ACTIVAR EL USUARIO ACTUAL"},
    };

    public static MessageDto CreateFromCode(string errorCode)
    {

        if (!Errors.ContainsKey(errorCode)) return new MessageDto(code: "0000", description: "ERROR SIN CLASIFICAR");

        return new MessageDto(code: errorCode, description: Errors[errorCode].ToUpper());
    }
}
