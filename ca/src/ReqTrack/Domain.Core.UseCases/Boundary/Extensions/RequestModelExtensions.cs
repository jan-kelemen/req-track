using ReqTrack.Domain.Core.UseCases.Boundary.Requests;
using ReqTrack.Domain.Core.UseCases.Exceptions;

namespace ReqTrack.Domain.Core.UseCases.Boundary.Extensions
{
    internal static class RequestModelExtensions
    {
        public static void ValidateAndThrowOnInvalid(this RequestModel request)
        {
            if (!request.Validate(out var errors))
            {
                throw new RequestValidationException { ValidationErrors = errors };
            }
        }
    }
}
