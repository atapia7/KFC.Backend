using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/account")]
[ApiController]
public class CreateAccountController: ControllerBase
{
    private readonly ICreateAccountInputPort _inputPort;
    private readonly ICreateAccountOutputPort _outputPort;

    public CreateAccountController(
        ICreateAccountInputPort inputPort,
        ICreateAccountOutputPort outputPort
        )
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateAccountDto inputDto)
    {
        inputDto.setNameClaim(nameClaim: JwtUtility.GetUserNameFromJwt(HttpContext));
        await _inputPort.Handle(inputDto: inputDto);

        return ResponseHelper.GetActionResult(_outputPort);
    }
}
