using KFC.UseCases.DTOs.Output;
using KFC.UseCases.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFC.UseCases.OutputPort;

public interface IGetChannelAllOutputPort : IOutputPort<QueryResult<IEnumerable<ChannelDto>>?>
{
    Task HandleSuccess(IHandleSuccess<QueryResult<IEnumerable<ChannelDto>>> success);
    Task HandleFailure(IHandleFailure failure);
}
