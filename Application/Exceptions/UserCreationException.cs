using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http;

namespace Application.Exceptions;

public class UserCreationException : BaseException
{
    private static int _statusCode = StatusCodes.Status406NotAcceptable;
    
    protected UserCreationException(SerializationInfo info, StreamingContext context, List<string> errors) : 
        base(info, context, _statusCode, errors)
    {
    }

    public UserCreationException(string? message) : 
        base(message, _statusCode)
    {
    }

    public UserCreationException(string? message, List<string> errors) : 
        base(message, _statusCode, errors)
    {
    }

    public UserCreationException(string? message, Exception? innerException, List<string> errors) : 
        base(message, innerException, _statusCode, errors)
    {
    }
}