using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using TradingApp.Module.Quotes.Authentication.Configuration;
using TradingApp.TradingWebApi.ExtensionMethods;
using TradingApp.TradingWebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
    policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
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
app.UseExceptionHandler(builder => builder.UseExceptionHandlerMiddleware());
app.UseCors();
app.AddModules();
app.Run();
