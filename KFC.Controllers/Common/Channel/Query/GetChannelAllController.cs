using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/Channel")]
[ApiController]
public class GetChannelAllController: ControllerBase
{
    private readonly IGetChannelAllInputPort _inputPort;
    private readonly IGetChannelAllOutputPort _outputPort;

    public GetChannelAllController(
        IGetChannelAllInputPort inputPort,
        IGetChannelAllOutputPort outputPort
    )
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpGet("GetChannelAll")]
    public async Task<IActionResult> GetChannelAll([FromQuery] GetChannelAllDto inputDto)
    {
        inputDto.setNameClaim(nameClaim: JwtUtility.GetUserNameFromJwt(HttpContext));
        await _inputPort.Handle(inputDto: inputDto);

        return ResponseHelper.GetActionResult(_outputPort);
    }


}
