using System.Net.Mime;
using Microsoft.AspNetCore.Diagnostics;
using SwapFietsDemo.Api.Exceptions;

namespace SwapFietsDemo.Api.Extensions;

internal static class ErrorHandlingMiddleware
{
    internal static void UseErrorHandling(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    
                    context.Response.ContentType = MediaTypeNames.Text.Plain;
                    
                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    if (exceptionHandlerPathFeature?.Error is SwapException)
                    {
                        await context.Response.WriteAsync(exceptionHandlerPathFeature?.Error?.Message ?? "");
                    }
                });
            });

            app.UseHsts();
        }
    }
}