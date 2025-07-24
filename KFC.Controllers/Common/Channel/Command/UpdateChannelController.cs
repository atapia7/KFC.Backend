using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/Channel")]
[ApiController]
public class UpdateChannelController: ControllerBase
{
    private readonly IUpdateChannelInputPort _inputPort;
    private readonly IUpdateChannelOutputPort _outputPort;

    public UpdateChannelController(
        IUpdateChannelInputPort inputPort,
        IUpdateChannelOutputPort outputPort
    )
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpPut("{code}")]
    [Authorize]
    public async Task<IActionResult> Update(int code, [FromBody] UpdateChannelDto inputDto)
    {
        inputDto.setCode(code: code);
        inputDto.setNameClaim(nameClaim: JwtUtility.GetUserNameFromJwt(HttpContext));
        
        await _inputPort.Handle(inputDto: inputDto);

        return ResponseHelper.GetActionResult(_outputPort);

    }
}
