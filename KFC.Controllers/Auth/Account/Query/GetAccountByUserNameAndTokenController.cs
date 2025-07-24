using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/account")]
[ApiController]
public class GetAccountByUserNameAndTokenController : ControllerBase
{
    private readonly IGetAccountByUserNameAndTokenInputPort _inputPort;
    private readonly IGetAccountByUserNameAndTokenOutputPort _outputPort;

    public GetAccountByUserNameAndTokenController(
        IGetAccountByUserNameAndTokenInputPort inputPort,
        IGetAccountByUserNameAndTokenOutputPort outputPort
    )
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpGet("GetAccountByUserNameAndToken")]
    public async Task<IActionResult> GetAccountByUserName([FromQuery] GetAccountByUserNameAndTokenDto inputDto)
    {
            inputDto.setNameClaim(nameClaim: JwtUtility.GetUserNameFromJwt(HttpContext));
        await _inputPort.Handle(inputDto: inputDto);

        return ResponseHelper.GetActionResult(_outputPort);
    }
}
