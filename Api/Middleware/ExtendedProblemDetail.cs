using Hellang.Middleware.ProblemDetails;

namespace Api.Middleware;

public class ExtendedProblemDetail : StatusCodeProblemDetails
{
    public ExtendedProblemDetail(int statusCode) : base(statusCode)
    {
    }

    public List<string>? Errors { get; set; }
}