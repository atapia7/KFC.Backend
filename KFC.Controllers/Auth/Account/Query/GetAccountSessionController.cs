using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/account")]
[ApiController]
public class GetAccountSessionController: ControllerBase
{
    private readonly IGetAccountSessionInputPort _inputPort;
    private readonly IGetAccountSessionOutputPort _outputPort;

    public GetAccountSessionController(
        IGetAccountSessionInputPort inputPort,
        IGetAccountSessionOutputPort outputPort
    )
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpGet("GetAccountSession")]
    [Authorize]
    public async Task<IActionResult> GetAccountSession()
    {
        GetSessionDto session = new GetSessionDto();
        session.setNameClaim(nameClaim: JwtUtility.GetUserNameFromJwt(HttpContext));
        await _inputPort.Handle(session);

        return ResponseHelper.GetActionResult(_outputPort);

    }
}
