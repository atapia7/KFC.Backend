using Microsoft.AspNetCore.Mvc;
using KFC.UseCases.DTOs.Interface;

namespace KFC.Controllers;

public static class ResponseHelper
{
	public static IActionResult GetActionResult(IHttpStatus output)
	{
		if (output.HttpStatusCode == System.Net.HttpStatusCode.OK)
			return new OkObjectResult(output);

        if (output.HttpStatusCode == System.Net.HttpStatusCode.Created)
            return new OkObjectResult(output);

        if (output.HttpStatusCode == System.Net.HttpStatusCode.Forbidden)
			return new BadRequestObjectResult(output);               
        
		if (output.HttpStatusCode == System.Net.HttpStatusCode.NoContent)
			return new NoContentResult();

		if (output.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
			return new NotFoundObjectResult(output);

		if (output.HttpStatusCode == System.Net.HttpStatusCode.BadRequest)
			return new BadRequestObjectResult(output);      

		return new BadRequestObjectResult(output);      

	}
}