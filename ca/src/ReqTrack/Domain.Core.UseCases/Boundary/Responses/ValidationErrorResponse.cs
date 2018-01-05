﻿using System.Collections.Generic;

namespace ReqTrack.Domain.Core.UseCases.Boundary.Responses
{
    public class ValidationErrorResponse : ResponseModel
    {
        public ValidationErrorResponse()
        {
        }

        public IReadOnlyDictionary<string, string> ValidationErrors { get; internal set; }
    }
}
