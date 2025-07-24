using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/account")]
[ApiController]
public class EnableAccountController : ControllerBase
{
    private readonly IEnableAccountInputPort _inputPort;
    private readonly IEnableAccountOutputPort _outputPort;

    public EnableAccountController(
        IEnableAccountInputPort inputPort,
        IEnableAccountOutputPort outputPort
        )
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpPatch("{username}/enable")]
    [Authorize]
    public async Task<IActionResult> Enable(string username)
    {
        EnableAccountDto inputDto = new EnableAccountDto();
        inputDto.UserName = username;
        inputDto.setNameClaim(nameClaim: JwtUtility.GetUserNameFromJwt(HttpContext));

        await _inputPort.Handle(inputDto: inputDto);

        return ResponseHelper.GetActionResult(_outputPort);
    }
}
