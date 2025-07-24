using KFC.Entities.Utility;
using KFC.UseCases.DTOs;
using KFC.UseCases.DTOs.Input;


namespace KFC.UseCases.Validator;

public class QueryValidator { 


    public static List<MessageDto> Validate(GetQueryDto queryDto)
    {
        var result = new List<MessageDto>();

        if (queryDto.Page <= 0) result.Add(new MessageDto("La pagina actual tiene que ser mKFCor a cero"));
        if (queryDto.PageSize <= 0) result.Add(new MessageDto("La cantidad de paginas tiene que ser mKFCor a cero"));

        return result;
    }



}
