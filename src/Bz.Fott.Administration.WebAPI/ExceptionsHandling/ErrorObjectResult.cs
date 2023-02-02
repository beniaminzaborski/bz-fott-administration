using Bz.Fott.Administration.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Bz.Fott.Administration.WebAPI.ExceptionsHandling;

internal class ErrorObjectResult : ObjectResult
{
    public ErrorObjectResult(object error, int statusCode)
                : base(error)
    {
        StatusCode = statusCode;
    }

    public static ErrorObjectResult Create(Exception exception)
    {
        var message = exception.Message;
        var httpStatusCode = (int)HttpStatusCode.InternalServerError;

        switch (exception)
        {
            case ValidationException:
                httpStatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case NotFoundException:
                httpStatusCode = (int)HttpStatusCode.NotFound;
                break;
            case InvalidOperationException:
                httpStatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        var errorResponse = new ErrorResponseDto
        {
            Message = message
        };

        return new ErrorObjectResult(errorResponse, httpStatusCode);
    }
}
