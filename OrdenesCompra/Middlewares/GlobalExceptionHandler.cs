﻿using Domain.Exceptions;
using LoggingService;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

internal sealed class GlobalExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILoggerManager logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError($"Unhandled exception occurred: {exception.Message}");

        httpContext.Response.StatusCode = exception switch
        {
            ApplicationException => StatusCodes.Status400BadRequest,
            EmailAlreadyExistsException => StatusCodes.Status409Conflict,
            PhoneAlreadyExistsException => StatusCodes.Status409Conflict,
            CustomerNotFoundException => StatusCodes.Status404NotFound,
            ProductNameAlreadyExistsException => StatusCodes.Status409Conflict,
            ProductNotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Type = exception.GetType().Name,
                Title = "An error occured",
                Detail = exception.Message
            }
        });
    }
}