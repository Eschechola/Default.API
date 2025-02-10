using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Otanimes.ApplicationServices.VIewModels.Generic;

namespace Otanimes.API.Middlewares;

public static class ExceptionMiddleware
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
            
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (contextFeature is null)
                    return;
            
                await context.Response.WriteAsJsonAsync(new ResponseViewModel<object>
                {
                    Message = "An internal server error has been occured, please try again.",
                    Data = new object(),
                });
            });
        });
    }
}