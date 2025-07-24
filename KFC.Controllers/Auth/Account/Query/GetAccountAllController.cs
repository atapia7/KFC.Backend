using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/account")]
[ApiController]
public class GetAccountAllController: ControllerBase
{
    private readonly IGetAccountAllInputPort _inputPort;
    private readonly IGetAccountAllOutputPort _outputPort;

    public GetAccountAllController(
        IGetAccountAllInputPort inputPort,
        IGetAccountAllOutputPort outputPort
    )
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpGet("GetAccountAll")]
    [Authorize]
    public async Task<IActionResult> GetAccountAll([FromQuery] GetAccountAllDto inputDto)
    {
        inputDto.setNameClaim(nameClaim: JwtUtility.GetUserNameFromJwt(HttpContext));
        await _inputPort.Handle(inputDto: inputDto);

        return ResponseHelper.GetActionResult(_outputPort);
    }


}
