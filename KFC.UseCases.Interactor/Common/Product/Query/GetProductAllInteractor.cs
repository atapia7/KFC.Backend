using KFC.UseCases.Interface;
using KFC.UseCases.Query;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.DTOs.Output;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;
using KFC.Entities;

namespace KFC.UseCases.Interactor;

public class GetProductAllInteractor : IGetProductAllInputPort
{
    private readonly IGetProductAllOutputPort _outputPort;
    private readonly IProductRepository _repository;
    private readonly IInputPortValidator<GetProductAllDto> _validator;

    public GetProductAllInteractor(
        IGetProductAllOutputPort outputPort, 
        IProductRepository repository,
        IInputPortValidator<GetProductAllDto> validator
   )
    {
        _outputPort = outputPort;
        _repository = repository;
        _validator = validator;
    }

    public async Task Handle(GetProductAllDto inputDto)
    {

        bool IsValid = await _validator.IsValid(inputDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }

        ProductAllFilter queryFilterDto = new ProductAllFilter();
        queryFilterDto.Name = string.IsNullOrEmpty(inputDto.FilterByName) ? null :inputDto.FilterByName.ToLower();

        var Products = await _repository.GetProductAllAsync(page: inputDto.Page, pageSize: inputDto.PageSize, filter: queryFilterDto);

        List<ProductDto> ProductsDto = new List<ProductDto>();
        foreach (var Product in Products.Results)
        {
            ProductDto ProductDto = new ProductDto();
            ProductDto.ProductId = Product.ProductId;
            ProductDto.Name = Product.Name;
            ProductDto.Description = Product.Description;
            ProductsDto.Add(ProductDto);
        }
        
        var resultDto = QueryResult<IEnumerable<ProductDto>>.Success(
            results:ProductsDto, 
            totalCount: Products.TotalCount, 
            totalPages: Products.TotalPages, 
            pageNumber: inputDto.Page, 
            pageSize: inputDto.PageSize
        );

        await _outputPort.HandleSuccess(HandleSuccess<QueryResult<IEnumerable<ProductDto>>>.Ok(data: resultDto));

    }
    
}

