using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/channel")]
[ApiController]
public class CreateChannelController: ControllerBase
{
    private readonly ICreateChannelInputPort _inputPort;
    private readonly ICreateChannelOutputPort _outputPort;

    public CreateChannelController(
        ICreateChannelInputPort inputPort,
        ICreateChannelOutputPort outputPort
    )
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateChannelDto inputDto)
    {
        inputDto.setNameClaim(nameClaim: JwtUtility.GetUserNameFromJwt(HttpContext));
        await _inputPort.Handle(inputDto: inputDto);

        return ResponseHelper.GetActionResult(_outputPort);
    }
}
