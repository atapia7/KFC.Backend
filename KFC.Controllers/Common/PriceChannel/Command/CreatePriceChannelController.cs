using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/PriceChannel")]
[ApiController]
public class CreatePriceChannelController: ControllerBase
{
    private readonly ICreatePriceChannelInputPort _inputPort;
    private readonly ICreatePriceChannelOutputPort _outputPort;

    public CreatePriceChannelController(
        ICreatePriceChannelInputPort inputPort,
        ICreatePriceChannelOutputPort outputPort
    )
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreatePriceChannelDto inputDto)
    {
        inputDto.setNameClaim(nameClaim: JwtUtility.GetUserNameFromJwt(HttpContext));
        await _inputPort.Handle(inputDto: inputDto);

        return ResponseHelper.GetActionResult(_outputPort);
    }
}
