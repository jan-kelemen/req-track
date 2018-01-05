namespace ReqTrack.Domain.Core.UseCases.Boundary.Responses
{
    public abstract class ResponseModel
    {
        protected ResponseModel(string message = null)
        {
            Message = message;
        }

        public string Message { get; internal set; }
    }
}
