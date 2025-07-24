using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/PriceChannel")]
[ApiController]
public class GetPriceChannelAllController: ControllerBase
{
    private readonly IGetPriceChannelAllInputPort _inputPort;
    private readonly IGetPriceChannelAllOutputPort _outputPort;

    public GetPriceChannelAllController(
        IGetPriceChannelAllInputPort inputPort,
        IGetPriceChannelAllOutputPort outputPort
    )
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpGet("GetPriceChannelAll")]
    public async Task<IActionResult> GetPriceChannelAll([FromQuery] GetPriceChannelAllDto inputDto)
    {
        inputDto.setNameClaim(nameClaim: JwtUtility.GetUserNameFromJwt(HttpContext));
        await _inputPort.Handle(inputDto: inputDto);

        return ResponseHelper.GetActionResult(_outputPort);
    }


}
