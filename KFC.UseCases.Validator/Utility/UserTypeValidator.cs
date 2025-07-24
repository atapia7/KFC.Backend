using KFC.Entities.Utility;
using KFC.UseCases.DTOs;


namespace KFC.UseCases.Validator;

public class UserTypeValidator { 


    public static List<MessageDto> Validate(string accountType)
    {
        var result = new List<MessageDto>();

        if (AccountTypeUtility.IsAdministrator(accountType))
            return result;
        
        if (AccountTypeUtility.IsCustomer(accountType))
            return result;
        
        if (AccountTypeUtility.IsSupervisor(accountType))
            return result;
        
        if (AccountTypeUtility.IsSeller(accountType))
            return result;
        
        result.Add(new MessageDto("El rol asignado no existe"));

        return result;
    }



}
