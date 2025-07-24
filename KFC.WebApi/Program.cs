using System.Text;
using KFC.Gateways.SQLite;
using KFC.Presenters;
using KFC.UseCases.Interactor;
using KFC.UseCases.Validator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization(); // ? Necesario para usar [Authorize]

builder.Services.AddRepositoriesSQLite(builder.Configuration);
builder.Services.AddInteractors();
builder.Services.AddPresenters();
builder.Services.AddValidators();


builder.Services.AddControllers();
// Configurar autenticaci�n JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    string secretKey = "aapKey";  // Cambiar por una clave segura en un entorno seguro
    byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);

    byte[] adjustedKeyBytes = new byte[32];
    Array.Copy(keyBytes, adjustedKeyBytes, Math.Min(keyBytes.Length, adjustedKeyBytes.Length));

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://yourdomain.com",
        ValidAudience = "https://api.yourdomain.com",
        IssuerSigningKey = new SymmetricSecurityKey(adjustedKeyBytes)
    };
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "KFC API Develop", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Bearer JWT Authorization header",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };
    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

