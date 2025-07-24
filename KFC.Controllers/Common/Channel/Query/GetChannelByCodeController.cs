using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/Channel")]
[ApiController]
public class GetChannelByCodeController: ControllerBase
{
    private readonly IGetChannelByCodeInputPort _inputPort;
    private readonly IGetChannelByCodeOutputPort _outputPort;

    public GetChannelByCodeController(
        IGetChannelByCodeInputPort inputPort,
        IGetChannelByCodeOutputPort outputPort
    )
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpGet("GetChannelByCode")]
    [Authorize]
    public async Task<IActionResult> GetChannelByCode([FromQuery] GetChannelByCodeDto inputDto)
    {
        inputDto.setNameClaim(nameClaim: JwtUtility.GetUserNameFromJwt(HttpContext));
        await _inputPort.Handle(inputDto: inputDto);

        return ResponseHelper.GetActionResult(_outputPort);
    }


}
