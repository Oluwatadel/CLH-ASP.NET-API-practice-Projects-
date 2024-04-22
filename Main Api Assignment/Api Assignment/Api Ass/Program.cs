using Api_Ass.Service;
using Api_Ass.Service.Implementation;
using Api_Ass.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var jwtOption = builder.Configuration.GetSection("Jwt");
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IContextService, ContextService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, //Validate user that generate token
            ValidateAudience = true, //Validate the recipient of the token is authorize to receive
            ValidateLifetime = true, //Check if token has not expiry and if sign in key is valid
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            //Validate the server (ValidateIssuer = true) that generates the token.
            //Validate the recipient of the token is authorized to receive (ValidateAudience = true)
            //Check if the token is not expired and the signing key of the issuer is valid(ValidateLifetime = true)
            //Validate signature of the token (ValidateIssuerSigningKey = true)
            //Additionally, we specify the values for the issuer, audience, signing key. In this example, I have stored these values in appsettings.json file.
            //Note: the signing key validate the issuer of the token
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
        options.Events = new JwtBearerEvents
        {
            OnChallenge = (context) =>
            {
                return Task.CompletedTask;
            },
            OnForbidden = (context) =>
            {
                return Task.CompletedTask;
            },
            OnAuthenticationFailed = (context) =>
            {
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Assignment", Version = "v1" });

    //configuring swagger to include security definitions
    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter a valid Token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    //Configure swagger to use JWT bearer token authentication
    o.AddSecurityRequirement(
        new OpenApiSecurityRequirement
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
                new string[] {}
            }
        });
});



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
