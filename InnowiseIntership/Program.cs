using AspNetCoreRateLimit;
using InnowiseIntership;
using InnowiseIntership.ActionFilters;
using InnowiseIntership.Extensions;
using Microsoft.AspNetCore.Mvc;
using CQRS;

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

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(CQRS.Reference).Assembly);
});

builder.Services.AddControllers(config =>
{
    
});

builder.Services.AddMemoryCache();
builder.Services.AddRateLimit();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCaching();
builder.Services.AddCacheHeaders();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.ConfigureExceptionHandling();
app.UseIpRateLimiting();

app.UseHttpsRedirection();
app.UseHttpCacheHeaders();

app.MapControllers();

app.Run();

