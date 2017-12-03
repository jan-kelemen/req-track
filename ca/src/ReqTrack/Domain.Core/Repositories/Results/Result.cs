namespace ReqTrack.Domain.Core.Repositories.Results
{
    /// <summary>
    /// Base class for repository results.
    /// </summary>
    public class Result
    {
        private readonly bool _isSuccessfull;

        /// <summary>
        /// Constructs the result.
        /// </summary>
        /// <param name="isSuccessfull">Indication if the operation was successfull.</param>
        public Result(bool isSuccessfull)
        {
            _isSuccessfull = isSuccessfull;
        }

        /// <summary>
        /// Indication if the operation was successfull.
        /// </summary>
        public bool IsSuccessfull => _isSuccessfull;

        public static implicit operator bool(Result result)
        {
            return result.IsSuccessfull;
        }
    }
}
