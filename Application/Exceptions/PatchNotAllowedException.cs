using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http;

namespace Application.Exceptions;

public class PatchNotAllowedException : BaseException
{
    private static int _statusCode = StatusCodes.Status403Forbidden;
    
    protected PatchNotAllowedException(SerializationInfo info, StreamingContext context, List<string> errors) 
        : base(info, context, _statusCode, errors)
    {
    }

    public PatchNotAllowedException(string? message) 
        : base(message, _statusCode)
    {
    }

    public PatchNotAllowedException(string? message, List<string> errors) 
        : base(message, _statusCode, errors)
    {
    }

    public PatchNotAllowedException(string? message, Exception? innerException, List<string> errors) 
        : base(message, innerException, _statusCode, errors)
    {
    }
}