using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/Product")]
[ApiController]
public class GetProductAllController: ControllerBase
{
    private readonly IGetProductAllInputPort _inputPort;
    private readonly IGetProductAllOutputPort _outputPort;

    public GetProductAllController(
        IGetProductAllInputPort inputPort,
        IGetProductAllOutputPort outputPort
    )
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpGet("GetProductAll")]
    public async Task<IActionResult> GetProductAll([FromQuery] GetProductAllDto inputDto)
    {
        inputDto.setNameClaim(nameClaim: JwtUtility.GetUserNameFromJwt(HttpContext));
        await _inputPort.Handle(inputDto: inputDto);

        return ResponseHelper.GetActionResult(_outputPort);
    }


}
