using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/account")]
[ApiController]
public class GetAccountByUserNameController : ControllerBase
{
    private readonly IGetAccountByUserNameInputPort _inputPort;
    private readonly IGetAccountByUserNameOutputPort _outputPort;

    public GetAccountByUserNameController(
        IGetAccountByUserNameInputPort inputPort,
        IGetAccountByUserNameOutputPort outputPort
    )
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpGet("GetAccountByUserName")]
    [Authorize]
    public async Task<IActionResult> GetAccountByUserName([FromQuery] GetAccountByUserNameDto inputDto)
    {
        inputDto.setNameClaim(nameClaim: JwtUtility.GetUserNameFromJwt(HttpContext));
        await _inputPort.Handle(inputDto: inputDto);

        return ResponseHelper.GetActionResult(_outputPort);
    }
}
