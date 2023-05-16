using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using GameBox.Application.Exceptions;
using System;
using System.Net;
using System.Linq;

namespace GameBox.Api.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is ValidationException)
        {
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new JsonResult(new
            {
                error = ((ValidationException)context.Exception).Failures.Values.ToArray()
            });

            return;
        }

        if (context.Exception is NotFoundException)
        {
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            context.Result = new JsonResult(new
            {
                error = new[] { context.Exception.Message }
            });

            return;
        }

        string message = context.Exception.Message;

        if (context.Exception.InnerException != null)
        {
            message = context.Exception.InnerException.Message;
        }

        context.HttpContext.Response.ContentType = "application/json";
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new JsonResult(new
        {
            error = new[] { message }
        });
    }
}
