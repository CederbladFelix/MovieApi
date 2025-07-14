using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Movies.Core.Exceptions;

namespace Movies.Api.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    var problemDetailsFactory = app.Services.GetRequiredService<ProblemDetailsFactory>();

                    int statusCode;
                    ProblemDetails problemDetails;

                    switch (contextFeature.Error)
                    {
                        case MovieNotFoundException movieNotFoundException:
                            statusCode = StatusCodes.Status404NotFound;
                            problemDetails = problemDetailsFactory.CreateProblemDetails
                            (
                                context,
                                statusCode,
                                title: movieNotFoundException.Title,
                                detail: movieNotFoundException.Message,
                                instance: context.Request.Path
                            );
                            break;
                        default:
                            statusCode = StatusCodes.Status500InternalServerError;
                            problemDetails = problemDetailsFactory.CreateProblemDetails
                            (
                                context,
                                statusCode,
                                title: "Internal server error",
                                detail: contextFeature.Error.Message,
                                instance: context.Request.Path
                            );
                            break;
                    }

                    context.Response.StatusCode = statusCode;
                    await context.Response.WriteAsJsonAsync(problemDetails);
                }
            });
        }
    }
}
