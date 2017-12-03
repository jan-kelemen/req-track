namespace ReqTrack.Domain.Core.Repositories.Results
{
    /// <summary>
    /// Result for actions which create entities.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public class CreateResult<T> : Result
    {
        private readonly T _created;

        /// <summary>
        /// Constructs the result.
        /// </summary>
        /// <param name="isSuccessfull">Indication if the operation was successfull.</param>
        /// <param name="created">Created entity or null.</param>
        public CreateResult(bool isSuccessfull, T created)
            : base(isSuccessfull)
        {
            _created = created;
        }

        /// <summary>
        /// Created entity. Value is set to null if operation wasn't successfull.
        /// </summary>
        public T Created => _created;
    }
}
