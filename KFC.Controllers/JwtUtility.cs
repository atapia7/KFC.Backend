using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;


namespace KFC.Controllers;

public static class JwtUtility
{
	public static string GetUserNameFromJwt(HttpContext httpContext)
	{
		try
		{
			// Obtener el token del encabezado de autorización
			var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Substring("Bearer ".Length).Trim();

			if (string.IsNullOrEmpty(token))
			{
				return null; // No se proporcionó un token válido
			}

			// Leer y decodificar el token JWT
			var tokenHandler = new JwtSecurityTokenHandler();
			var jwtToken = tokenHandler.ReadJwtToken(token);

			// Obtener el valor del claim del nombre de usuario
			var userName = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

			return userName;
		}
		catch (Exception)
		{
			return null; // Ocurrió un error al procesar el token JWT
		}
	}
}