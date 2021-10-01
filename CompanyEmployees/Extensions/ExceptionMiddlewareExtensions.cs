using Contracts;
using Entities.ErrorModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CompanyEmployees.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigExceptionHandler( this IApplicationBuilder app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appErr =>
            {
                appErr.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature !=null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        var ed = new ErrorDetails();
                        ed.StatusCode = context.Response.StatusCode;
                        ed.Message = "Internal Server Error.";
                        await context.Response.WriteAsync(ed.ToString());
                        ;
                    }
                });
            });
        }
    }
}
