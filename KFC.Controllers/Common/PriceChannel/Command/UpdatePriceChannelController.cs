using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/PriceChannel")]
[ApiController]
public class UpdatePriceChannelController: ControllerBase
{
    private readonly IUpdatePriceChannelInputPort _inputPort;
    private readonly IUpdatePriceChannelOutputPort _outputPort;

    public UpdatePriceChannelController(
        IUpdatePriceChannelInputPort inputPort,
        IUpdatePriceChannelOutputPort outputPort
    )
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpPut("{code}")]
    [Authorize]
    public async Task<IActionResult> Update(int code, [FromBody] UpdatePriceChannelDto inputDto)
    {
        inputDto.setCode(code: code);
        inputDto.setNameClaim(nameClaim: JwtUtility.GetUserNameFromJwt(HttpContext));
        
        await _inputPort.Handle(inputDto: inputDto);

        return ResponseHelper.GetActionResult(_outputPort);

    }
}
