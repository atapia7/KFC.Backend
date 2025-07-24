using KFC.UseCases.DTOs.Output;
using KFC.UseCases.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFC.UseCases.OutputPort;

public interface IGetChannelByCodeOutputPort : IOutputPort<ChannelDto?>
{
    Task HandleSuccess(IHandleSuccess<ChannelDto> success);
    Task HandleFailure(IHandleFailure failure);
}
