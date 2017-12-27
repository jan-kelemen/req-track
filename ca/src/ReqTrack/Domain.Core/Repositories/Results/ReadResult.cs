namespace ReqTrack.Domain.Core.Repositories.Results
{
    /// <summary>
    /// Result for actions which read entities.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public class ReadResult<T> : Result
    {
        private readonly T _read;

        /// <summary>
        /// Constructs the result.
        /// </summary>
        /// <param name="isSuccessfull">Indication if the operation was successfull.</param>
        /// <param name="read">Read entity or null.</param>
        public ReadResult(bool isSuccessfull, T read) : base(isSuccessfull)
        {
            _read = read;
        }

        /// <summary>
        /// Read entity. Value is set to null if operation wasn't successfull.
        /// </summary>
        public T Read => _read;
    }
}
