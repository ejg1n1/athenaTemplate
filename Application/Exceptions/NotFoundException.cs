using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http;

namespace Application.Exceptions;

public class NotFoundException : BaseException
{
    private static int _statusCode = StatusCodes.Status404NotFound;

    protected NotFoundException(SerializationInfo info, StreamingContext context, List<string> errors) : 
        base(info, context, _statusCode, errors )
    {
        
    }

    public NotFoundException(string? message) : base(message, _statusCode)
    {
    }
    
    public NotFoundException(string? message, List<string> errors) : base(message, _statusCode, errors) {}

    public NotFoundException(string? message, Exception? innerException, List<string> errors) :
        base(message, innerException, _statusCode, errors)
    {
    }
}