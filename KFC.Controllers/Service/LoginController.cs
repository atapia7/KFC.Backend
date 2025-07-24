using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.Controllers;

[Route("api/base")]
[ApiController]
public class LoginController: ControllerBase
{
	private readonly ILoginInputPort _inputPort;
	private readonly ILoginOutputPort _outputPort;

	public LoginController(
		ILoginInputPort inputPort,
		ILoginOutputPort outputPort)
	{
		_inputPort = inputPort;
		_outputPort = outputPort;
	}

	[HttpPost("authentication")]
	public  async Task<IActionResult> Authentication([FromBody] LoginDto auth)
	{
		await _inputPort.Handle(auth);

		return ResponseHelper.GetActionResult(_outputPort);

    }	
}