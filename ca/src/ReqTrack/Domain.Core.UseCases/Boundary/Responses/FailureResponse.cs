﻿namespace ReqTrack.Domain.Core.UseCases.Boundary.Responses
{
    public class FailureResponse : ResponseModel
    {
        public FailureResponse(string message) : base(message)
        {
        }
    }
}
