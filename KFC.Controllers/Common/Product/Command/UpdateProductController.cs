using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/Product")]
[ApiController]
public class UpdateProductController: ControllerBase
{
    private readonly IUpdateProductInputPort _inputPort;
    private readonly IUpdateProductOutputPort _outputPort;

    public UpdateProductController(
        IUpdateProductInputPort inputPort,
        IUpdateProductOutputPort outputPort
    )
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpPut("{code}")]
    [Authorize]
    public async Task<IActionResult> Update(int code, [FromBody] UpdateProductDto inputDto)
    {
        inputDto.setCode(code: code);
        inputDto.setNameClaim(nameClaim: JwtUtility.GetUserNameFromJwt(HttpContext));
        
        await _inputPort.Handle(inputDto: inputDto);

        return ResponseHelper.GetActionResult(_outputPort);

    }
}
