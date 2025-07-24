using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/account")]
[ApiController]
public class UpdatePasswordAccountController: ControllerBase
{
    private readonly IUpdatePasswordAccountInputPort _inputPort;
    private readonly IUpdatePasswordAccountOutputPort _outputPort;

    public UpdatePasswordAccountController(
        IUpdatePasswordAccountInputPort inputPort,
        IUpdatePasswordAccountOutputPort outputPort
        )
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpPatch("{username}/change-password")]
    
    public async Task<IActionResult> Update(string username, [FromBody] UpdatePasswordAccountDto inputDto)
    {
        inputDto.setUserName(username);
        await _inputPort.Handle(inputDto: inputDto);

        return ResponseHelper.GetActionResult(_outputPort);
    }
}
