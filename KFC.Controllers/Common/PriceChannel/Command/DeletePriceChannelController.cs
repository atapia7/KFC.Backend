using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/PriceChannel")]
[ApiController]
public class DeletePriceChannelController: ControllerBase
{
    private readonly IDeletePriceChannelInputPort _inputPort;
    private readonly IDeletePriceChannelOutputPort _outputPort;

    public DeletePriceChannelController(
        IDeletePriceChannelInputPort inputPort,
        IDeletePriceChannelOutputPort outputPort
    )
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpDelete("{code}")]
    [Authorize]
    public async Task<IActionResult> Delete(int code)
    {
        DeletePriceChannelDto inputDto = new DeletePriceChannelDto();
        inputDto.PriceChannelCode = code;
        inputDto.setNameClaim(nameClaim: JwtUtility.GetUserNameFromJwt(HttpContext));

        await _inputPort.Handle(inputDto: inputDto);

        return ResponseHelper.GetActionResult(_outputPort);
    }
}
