using KFC.UseCases.DTOs.Output;
using KFC.UseCases.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFC.UseCases.OutputPort;

public interface IGetProductAllOutputPort : IOutputPort<QueryResult<IEnumerable<ProductDto>>?>
{
    Task HandleSuccess(IHandleSuccess<QueryResult<IEnumerable<ProductDto>>> success);
    Task HandleFailure(IHandleFailure failure);
}
