using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/base")]
[ApiController]
public class SeedController : ControllerBase
{

    private readonly ISeedInputPort _inputPort;
    private readonly ISeedOutputPort _outputPort;

    public SeedController(
        ISeedInputPort inputPort,
        ISeedOutputPort outputPort)
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    [HttpPost("seed")]
    public async Task<IActionResult> Seed([FromBody] SeedDto seed)
    {
        await _inputPort.Handle(seed);

        return ResponseHelper.GetActionResult(_outputPort);

    }
}
