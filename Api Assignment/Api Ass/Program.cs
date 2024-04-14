using Api_Ass.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var jwtOption = builder.Configuration.GetSection("Jwt");
builder.Services.AddScoped<IIdentityService, IdentityService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, //Validate user that generate token
            ValidateAudience = true, //Validate the recipient of the token is authorize to receive
            ValidateLifetime = true, //Check if token has not expiry and if sign in key is valid
            ValidIssuer = builder.Configuration["Jwt: Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            //Validate the server (ValidateIssuer = true) that generates the token.
            //Validate the recipient of the token is authorized to receive (ValidateAudience = true)
            //Check if the token is not expired and the signing key of the issuer is valid(ValidateLifetime = true)
            //Validate signature of the token (ValidateIssuerSigningKey = true)
            //Additionally, we specify the values for the issuer, audience, signing key. In this example, I have stored these values in appsettings.json file.
            //Note: the signing key validate the issuer of the token
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
