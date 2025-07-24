using KFC.Entities;
using KFC.Entities.Enums;
using KFC.Entities.Utility;
using KFC.UseCases.Query;

namespace KFC.UseCases.Interactor;

public class ProductAllFilter : QueryFilter<Product>
{
  
    public string? Name { get; set; }

    
    public override IQueryable<Product> Apply(IQueryable<Product> queryable)
    {
        if (Name is not null) 
        {
            queryable = queryable.Where(p=> p.Name.ToLower().Contains(Name));
        }           

        return queryable;
    }
}
    