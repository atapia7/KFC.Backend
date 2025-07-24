using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFC.UseCases.OutputPort;

public interface ICreateProductOutputPort : IOutputPort<string?>
{
    Task HandleSuccess(IHandleSuccess<string> success);
    Task HandleFailure(IHandleFailure failure);
}
