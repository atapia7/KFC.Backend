namespace KFC.UseCases.DTOs;

public class MessageDto
{

    public MessageDto(string code, string description)
    {
        Code = code;
        Description = description;
    }

    public MessageDto(string description)
    {
        Code = "0001";
        Description = $"Error en datos: {description}";
    }

    public string Code { get; internal set; }
    public string Description { get; internal set; }
}