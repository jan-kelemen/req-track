using ReqTrack.Domain.Core.Entities.Interfaces;

namespace ReqTrack.Domain.Core.Entities
{
    /// <summary>
    /// <c>Entity</c> is a base class for all entities in the domain model.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public abstract class Entity<T> : IEntity<T> where T : IEntity<T>
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

        public Identity Id => _id;

        public bool HasBlankIdentity() => _id.IsBlankIdentity();

        public bool HasSameIdentityAs(T other) => Equals(other);

        public abstract bool HasSameValueAs(T other);

        public override sealed bool Equals(object obj)
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

        public override sealed int GetHashCode() => _id.GetHashCode();

        public override string ToString() => $"ID={_id.ToString()}";
    }
}
