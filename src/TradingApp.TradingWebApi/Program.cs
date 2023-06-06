using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using TradingApp.Application.Authentication.Configuration;
using TradingApp.TradingWebApi.ExtensionMethodes;
using TradingApp.TradingWebApi.Middlewares;
using TradingApp.TradingWebApi.Modules;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
    policy =>
    {
        policy.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
builder.Services.AddSwagger();
builder.Services.AddServices(builder.Configuration);
builder.AddLogging();
builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.AddTransient<IConfigureOptions<JwtBearerOptions>, JwtBearerOptionsSetup>();
builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();



app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;

});
app.UseHttpsRedirection();
app.UseExceptionHandler(app => app.UseExceptionHandlerMiddleware());

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.AddAuthenticationModule();
app.AddTestModule();
app.AddStooqModule();
app.Run();
