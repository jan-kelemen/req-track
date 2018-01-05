using System.Collections.Generic;

namespace ReqTrack.Domain.Core.UseCases.Boundary.Responses
{
    public class ValidationErrorResponse : ResponseModel
    {
        public ValidationErrorResponse(IReadOnlyDictionary<string, string> errors, string message = null) : base(message)
        {
            ValidationErrors = errors;
        }

        public IReadOnlyDictionary<string, string> ValidationErrors { get; }
    }
}
