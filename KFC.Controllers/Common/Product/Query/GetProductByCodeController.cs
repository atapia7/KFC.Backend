using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/Product")]
[ApiController]
public class GetProductByCodeController: ControllerBase
{
    private readonly IGetProductByCodeInputPort _inputPort;
    private readonly IGetProductByCodeOutputPort _outputPort;

    public GetProductByCodeController(
        IGetProductByCodeInputPort inputPort,
        IGetProductByCodeOutputPort outputPort
    )
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpGet("GetProductByCode")]
    [Authorize]
    public async Task<IActionResult> GetProductByCode([FromQuery] GetProductByCodeDto inputDto)
    {
        inputDto.setNameClaim(nameClaim: JwtUtility.GetUserNameFromJwt(HttpContext));

        await _inputPort.Handle(inputDto: inputDto);

        return ResponseHelper.GetActionResult(_outputPort);
    }


}
