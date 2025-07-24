using KFC.Entities.Enums;

namespace KFC.Entities.Utility;

public class StatusTypeUtility
{
    private const string REGISTERED = "REGISTRADO";
    private const string ENABLE = "ENABLE";
    private const string DISABLE = "DISABLE";

    public static StatusEnum? ConverTo(string statustype)
    {
        if (string.IsNullOrEmpty(statustype)) return null;

        if (statustype.ToUpper() == REGISTERED) return StatusEnum.Registered;
        else if (statustype.ToUpper() == ENABLE) return StatusEnum.Enabled;
        else if (statustype.ToUpper() == DISABLE) return StatusEnum.Disabled;

        return null;
    }

    public static string ToString(StatusEnum? statustype)
    {
        if (statustype is null) return "";

        if (statustype == StatusEnum.Registered) return REGISTERED ;
        if (statustype == StatusEnum.Enabled) return ENABLE ;
        if (statustype == StatusEnum.Disabled) return DISABLE ;

        return "";
    }

}
