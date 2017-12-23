﻿using System;

namespace ReqTrack.Domain.Core.Contexts
{
    /// <summary>
    /// <c>Entity</c> is a base class for all entities in the domain model.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public abstract class Entity<T> where T : class 
    {
        private Identity _id;

        /// <summary>
        /// Creates the entity.
        /// </summary>
        /// <param name="id">Entity identity.</param>
        protected Entity(Identity id)
        {
            _id = id;
        }

        /// <summary>
        /// Identity of the entity.
        /// </summary>
        public Identity Id => _id;

        /// <summary>
        /// Checks if a entity has blank identity.
        /// </summary>
        /// <returns><c>true</c> if a entity has blank identity.</returns>
        public bool HasBlankIdentity() => _id.IsBlankIdentity();

        /// <summary>
        /// Checks if entities have same identity.
        /// </summary>
        /// <param name="other">other entity.</param>
        /// <returns><c>true</c> if entities have same identity.</returns>
        public bool HasSameIdentityAs(T other) => Equals(other);

        /// <summary>
        /// Checks if entities have same value, not including the identity.
        /// </summary>
        /// <param name="other">other entity.</param>
        /// <returns><c>true</c> if entities have same value.</returns>
        public virtual bool HasSameValueAs(T other)
        {
            throw new NotImplementedException("Probably YAGNI");
        }

        /// <inheritdoc />
        public sealed override bool Equals(object obj)
        {
            var other = obj as Entity<T>;

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            return _id == other._id;
        }

        /// <inheritdoc />
        public sealed override int GetHashCode() => _id.GetHashCode();

        /// <inheritdoc />
        public override string ToString() => $"ID={_id.ToString()}";
    }
}
