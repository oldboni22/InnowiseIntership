using Exceptions;
using Exceptions.NotFound;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;

namespace InnowiseIntership.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((_, config) =>
            {
                config.WriteTo.File("logs/.log")
                    .WriteTo.Console();
                config.ReadFrom.Configuration(builder.Configuration);
            }
        );
    }

    public static void ConfigureExceptionHandling(this WebApplication app)
    {
        app.UseExceptionHandler(_ => _.Run(async context=>
        {
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null)
            {
                context.Response.StatusCode = contextFeature.Error switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError,
                };

                var details = new ErrorDetails
                {
                    StatusCode = context.Response.StatusCode,
                    Message = contextFeature.Error.Message
                };

                Log.Error(details.ToString());
                await context.Response.WriteAsync(details.ToString());

            }
        }));
    }
}