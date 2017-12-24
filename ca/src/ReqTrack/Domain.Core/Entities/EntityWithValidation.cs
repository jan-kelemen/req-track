using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ReqTrack.Domain.Core.Entities
{
    /// <summary>
    /// <c>EntityWithValidation</c> is a base class for entities of the domain model which have validation.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public abstract class EntityWithValidation<T> : Entity<T> where T : class 
    {
        private readonly Dictionary<string, Func<bool>> _validationFunctions = new Dictionary<string, Func<bool>>();

        private readonly Dictionary<string, string> _validationErrors = new Dictionary<string, string>();

        protected EntityWithValidation(Identity id) : base(id)
        {
        }

        /// <summary>
        /// Validates the entity using the preconfigured validation configuration.
        /// </summary>
        public bool IsValid => _validationErrors.Count == 0;

        /// <summary>
        /// Runs all registered validation functions.
        /// </summary>
        /// <returns><c>true</c> if the entity is valid.</returns>
        public bool Validate()
        {
            _validationErrors.Clear();
            foreach (var function in _validationFunctions.Values)
            {
                function();
            }

            return IsValid;
        }

        /// <summary>
        /// Detected validation errors.
        /// </summary>
        public IReadOnlyDictionary<string, string> ValidationErrors => _validationErrors;

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

        /// <summary>
        /// Removes all keys starting with key.
        /// </summary>
        /// <param name="keyStart">Beggining of the key string.</param>
        /// <returns><c>true</c> if atleast one error was removed.</returns>
        protected bool RemoveErrorsStartingWithKey(string keyStart)
        {
            var anyRemoved = false;
            foreach (var validationError in _validationErrors.Keys.ToImmutableList())
            {
                if (validationError.StartsWith(keyStart))
                {
                    anyRemoved |= RemoveError(validationError);
                }
            }

            return anyRemoved;
        }

        /// <summary>
        /// Registers a validation function of the entity.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="function"></param>
        /// <returns><c>true</c> if the validation function was successfully added.</returns>
        protected bool RegisterValidationFunction(string key, Func<bool> function)
        {
            return _validationFunctions.TryAdd(key, function);
        }

        /// <summary>
        /// Executes the common remove error - check - add error pattern.
        /// </summary>
        /// <param name="key">Key of the error.</param>
        /// <param name="message">Message of the error.</param>
        /// <param name="result">Result of the error.</param>
        /// <returns></returns>
        protected bool GeneralValidationFunction(string key, string message, bool result)
        {
            RemoveError(key);
            if (!result)
            {
                AddModelError(key, message);
            }

            return result;
        }
    }
}
