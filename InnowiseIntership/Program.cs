using InnowiseIntership;
using InnowiseIntership.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureSerilog();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddRepositoryContext(builder.Configuration);
builder.Services.AddRepositoryManager();
builder.Services.AddServiceManager();

builder.Services.AddControllers();

var app = builder.Build();

app.ConfigureExceptionHandling();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();

