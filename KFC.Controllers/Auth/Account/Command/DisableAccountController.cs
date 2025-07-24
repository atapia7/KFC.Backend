using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/account")]
[ApiController]
public class DisableAccountController: ControllerBase
{
    private readonly IDisableAccountInputPort _inputPort;
    private readonly IDisableAccountOutputPort _outputPort;

    public DisableAccountController(
        IDisableAccountInputPort inputPort,
        IDisableAccountOutputPort outputPort
        )
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpPatch("{username}/disable")]
    [Authorize]
    public async Task<IActionResult> Disable(string username)
    {
        DisableAccountDto inputDto = new DisableAccountDto();
        inputDto.UserName = username;
        inputDto.setNameClaim(nameClaim: JwtUtility.GetUserNameFromJwt(HttpContext));

        await _inputPort.Handle(inputDto: inputDto);

        return ResponseHelper.GetActionResult(_outputPort);
    }
}
