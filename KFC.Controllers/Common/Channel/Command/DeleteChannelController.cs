using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/channel")]
[ApiController]
public class DeleteChannelController: ControllerBase
{
    private readonly IDeleteChannelInputPort _inputPort;
    private readonly IDeleteChannelOutputPort _outputPort;

    public DeleteChannelController(
        IDeleteChannelInputPort inputPort,
        IDeleteChannelOutputPort outputPort
    )
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpDelete("{code}")]
    [Authorize]
    public async Task<IActionResult> Delete(int code)
    {
        DeleteChannelDto inputDto = new DeleteChannelDto();
        inputDto.ChannelCode = code;
        inputDto.setNameClaim(nameClaim: JwtUtility.GetUserNameFromJwt(HttpContext));

        await _inputPort.Handle(inputDto: inputDto);

        return ResponseHelper.GetActionResult(_outputPort);
    }
}
