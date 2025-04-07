using InnowiseIntership;
using InnowiseIntership.ActionFilters;
using InnowiseIntership.Extensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureSerilog();

builder.Services.AddAuth0(builder.Configuration);
builder.Services.AddAuthentication();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddRepositoryContext(builder.Configuration);
builder.Services.AddRepositoryManager();
builder.Services.AddServiceManager();


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddScoped<ValidationFilter>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.ConfigureExceptionHandling();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();

