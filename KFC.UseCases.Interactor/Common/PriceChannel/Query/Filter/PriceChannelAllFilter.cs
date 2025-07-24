using KFC.Entities;
using KFC.Entities.Enums;
using KFC.Entities.Utility;
using KFC.UseCases.Query;

namespace KFC.UseCases.Interactor;

public class PriceChannelAllFilter : QueryFilter<PriceChannel>
{
  
    public bool? isActive { get; set; }

    
    public override IQueryable<PriceChannel> Apply(IQueryable<PriceChannel> queryable)
    {
        if (isActive is not null) 
        {
            queryable = queryable.Where(p=> p.IsActive == isActive);
        }           

        return queryable;
    }
}
    