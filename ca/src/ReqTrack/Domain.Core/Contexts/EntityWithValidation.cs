using System;
using System.Collections.Generic;

namespace ReqTrack.Domain.Core.Contexts
{
    /// <summary>
    /// <c>EntityWithValidation</c> is a base class for entities of the domain model which have validation.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public abstract class EntityWithValidation<T> : Entity<T> where T : class 
    {
        private readonly Dictionary<string, Func<T, string, bool>> _validationFunctions;

        private readonly Dictionary<string, string> _validationErrors = new Dictionary<string, string>();

        protected EntityWithValidation(Identity id, Dictionary<string, Func<T, string, bool>> validationFunctions)
            : base(id)
        {
            _validationFunctions = validationFunctions;
        }

        /// <summary>
        /// Information if the propery is valid, should be called after <see cref="Validate"/>.
        /// </summary>
        public bool IsValid => _validationErrors.Count == 0;

        /// <summary>
        /// Validates the entity using the preconfigured validation configuration.
        /// </summary>
        /// <returns>Dictionary with validation results, it's empty if the entity is valid.</returns>
        public IReadOnlyDictionary<string, string> Validate()
        {
            _validationFunctions.Clear();
            foreach (var function in _validationFunctions)
            {
                function.Value(this as T, function.Key);
            }

            return _validationErrors;
        }

        /// <summary>
        /// Should be called from specific validation functions.
        /// </summary>
        /// <param name="key">Key under which to store validation error.</param>
        /// <param name="value">Explanation of the validation error.</param>
        /// <returns><c>true</c> if the error was successfully added.</returns>
        protected bool AddModelError(string key, string value) => _validationErrors.TryAdd(key, value);

        /// <summary>
        /// Should be called from specific validation functions to remove an error if neccessary.
        /// </summary>
        /// <param name="key">Validation key to remove.</param>
        /// <returns><c>true</c> if the error was removed.</returns>
        protected bool RemoveError(string key) => _validationErrors.Remove(key);
    }
}
