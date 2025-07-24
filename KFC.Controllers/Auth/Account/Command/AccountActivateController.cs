using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;


namespace KFC.Controllers;

[Route("api/account")]
[ApiController]
public class AccountActivateController: ControllerBase
{
    private readonly IAccountActivateInputPort _inputPort;
    private readonly IAccountActivateOutputPort _outputPort;

    public AccountActivateController(
        IAccountActivateInputPort inputPort,
        IAccountActivateOutputPort outputPort
        )
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpPatch("{username}/activate")]
    public async Task<IActionResult> Activate(string username, [FromBody] AccountActivateDto inputDto)
    {
        inputDto.setUserName(username);
        await _inputPort.Handle(inputDto: inputDto);

        return ResponseHelper.GetActionResult(_outputPort);
    }
}
