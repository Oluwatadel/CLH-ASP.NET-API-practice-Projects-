using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using TicketingService.Adapters;
using TicketingService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(
//    option =>
//    {
//        option.Cookie.Name = "ticketSession";
//        option.Cookie.Expiration = TimeSpan.FromSeconds(5);
//    });
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme
    )
    .AddCookie(
    option =>
    {
        option.Cookie.Name = "ticketSession";
        option.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return Task.CompletedTask;
            },
            OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return Task.CompletedTask;
            }

        };
    }
);
builder.Services.AddScoped<JsonPlaceholderAdapter>();
builder.Services.AddTransient<IdentityService>();
builder.Services.AddTransient<PeopleManagementService>();
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
//app.UseSession();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
