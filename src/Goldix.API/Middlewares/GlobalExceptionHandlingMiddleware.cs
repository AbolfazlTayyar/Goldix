﻿using Goldix.Application.Exceptions;
using Goldix.Application.Wrappers;

namespace Goldix.API.Middlewares;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);

            if (context.Response.StatusCode == 401 || context.Response.StatusCode == 403)
                await HandleResponseAsync(context);
        }
        catch (Exception ex)
        {
            await HandleResponseAsync(context, ex);
        }
    }

    private static async Task HandleResponseAsync(HttpContext context, Exception exception = null)
    {
        if (context.Response.HasStarted)
            return;

        context.Response.ContentType = "application/json";
        ApiResponse<object> response = null;

        if (exception != null)
        {
            switch (exception)
            {
                case CustomValidationException validationEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = ExceptionResponse.CreateValidationResponse(validationEx);
                    break;
                case BadRequestException badRequestEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = ExceptionResponse.CreateBadRequestResponse(badRequestEx);
                    break;
                case NotFoundException notFoundEx:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response = ExceptionResponse.CreateNotFoundResponse(notFoundEx);
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = ExceptionResponse.CreateUnhandledErrorResponse(exception);
                    break;
            }
        }
        else
        {
            switch (context.Response.StatusCode)
            {
                case 401:
                    response = ExceptionResponse.CreateUnauthorizedResponse();
                    break;
                case 403:
                    response = ExceptionResponse.CreateForbiddenResponse();
                    break;
                default:
                    return;
            }
        }

        if (response is not null)
            await context.Response.WriteAsJsonAsync(response);
    }
}