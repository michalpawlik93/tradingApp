using TradingApp.TradingWebApi.ExtensionMethodes;
using TradingApp.TradingWebApi.Middlewares;
using TradingApp.TradingWebApi.Modules;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();
builder.AddLogging();
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.UseHttpsRedirection();
app.UseExceptionHandler(app => app.UseExceptionHandlerMiddleware());

app.AddAuthenticationModule();
app.Run();
