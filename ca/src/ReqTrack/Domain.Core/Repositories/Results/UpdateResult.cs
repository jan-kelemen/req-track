namespace ReqTrack.Domain.Core.Repositories.Results
{
    /// <summary>
    /// Result for actions which update entities.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public class UpdateResult<T> : Result
    {
        private readonly T _updated;

        /// <summary>
        /// Constructs the result.
        /// </summary>
        /// <param name="isSuccessfull">Indication if the operation was successfull.</param>
        /// <param name="updated">Updated entity or null.</param>
        public UpdateResult(bool isSuccessfull, T updated) : base(isSuccessfull)
        {
            _updated = updated;
        }

        /// <summary>
        /// Updated entity. Value is set to null if operation wasn't successfull.
        /// </summary>
        public T Updated => _updated;
    }
}
