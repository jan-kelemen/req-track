using System;
using System.Collections.Generic;
using System.Text;

namespace ReqTrack.Domain.Core.Repositories.Results
{
    /// <summary>
    /// Result for actions which delete entities.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public class DeleteResult<T> : Result
    {
        private readonly T _deleted;

        /// <summary>
        /// Constructs the result.
        /// </summary>
        /// <param name="isSuccessfull">Indication if the operation was successfull.</param>
        /// <param name="read">Read entity or null.</param>
        public DeleteResult(bool isSuccessfull, T deleted) : base(isSuccessfull)
        {
            _deleted = deleted;
        }

        /// <summary>
        /// Deleted entity or identity of the entity only.
        /// </summary>
        public T Deleted => _deleted;
    }
}
