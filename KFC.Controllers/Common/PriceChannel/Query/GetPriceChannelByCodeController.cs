using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/PriceChannel")]
[ApiController]
public class GetPriceChannelByCodeController: ControllerBase
{
    private readonly IGetPriceChannelByCodeInputPort _inputPort;
    private readonly IGetPriceChannelByCodeOutputPort _outputPort;

    public GetPriceChannelByCodeController(
        IGetPriceChannelByCodeInputPort inputPort,
        IGetPriceChannelByCodeOutputPort outputPort
    )
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpGet("GetPriceChannelByCode")]
    [Authorize]
    public async Task<IActionResult> GetPriceChannelByCode([FromQuery] GetPriceChannelByCodeDto inputDto)
    {
        inputDto.setNameClaim(nameClaim: JwtUtility.GetUserNameFromJwt(HttpContext));
        await _inputPort.Handle(inputDto: inputDto);

        return ResponseHelper.GetActionResult(_outputPort);
    }


}
