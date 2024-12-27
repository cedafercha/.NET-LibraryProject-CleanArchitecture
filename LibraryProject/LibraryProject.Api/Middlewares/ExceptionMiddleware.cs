using Microsoft.AspNetCore.Http;
using LibraryProject.Api.Models;
using LibraryProject.Domain.Exceptions;
using System;
using System.Text.Json;
using System.Threading.Tasks;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var errorResponse = GetErrorDetails(exception);
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = errorResponse.StatusCode;

        var response = new
        {
            message = errorResponse.ErrorMessage
        };

        var jsonResponse = JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(jsonResponse);
    }

    private static ErrorResponseModel GetErrorDetails(Exception contextFeature)
    {
        return contextFeature switch
        {
            // Loan doesn't exists
            NoLoanException noLoanException => new ErrorResponseModel
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = string.Format("The loan {0} does not exists", noLoanException.Message),
            },
            // book is already on loan
            BookLoanException bookLoanException => new ErrorResponseModel
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = string.Format("The book with id {0} is already on loan.", bookLoanException.Message),
            },
            // User already has one active loan
            UserLoanException userLoanException => new ErrorResponseModel
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = string.Format("The user with id {0} already has a book on loan, so another loan cannot be made.", userLoanException.Message),
            },
            // Exception Not Controlled => Internal Server Error
            _ => new ErrorResponseModel
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ErrorMessage = "Internal Error"
            },
        };
    }
}
