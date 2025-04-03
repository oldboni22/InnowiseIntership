using InnowiseIntership;
using InnowiseIntership.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureSerilog();

builder.Services.AddRepositoryManager();
builder.Services.AddServiceManager();

builder.Services.AddControllers();


var app = builder.Build();
app.ConfigureExceptionHandling();

app.MapControllers();
app.UseHttpsRedirection();
app.Run();
