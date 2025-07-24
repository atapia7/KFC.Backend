using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/Product")]
[ApiController]
public class DeleteProductController: ControllerBase
{
    private readonly IDeleteProductInputPort _inputPort;
    private readonly IDeleteProductOutputPort _outputPort;

    public DeleteProductController(
        IDeleteProductInputPort inputPort,
        IDeleteProductOutputPort outputPort
    )
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpDelete("{code}")]
    [Authorize]
    public async Task<IActionResult> Delete(int code)
    {
        DeleteProductDto inputDto = new DeleteProductDto();
        inputDto.ProductCode = code;
        inputDto.setNameClaim(nameClaim: JwtUtility.GetUserNameFromJwt(HttpContext));

        await _inputPort.Handle(inputDto: inputDto);

        return ResponseHelper.GetActionResult(_outputPort);
    }
}
