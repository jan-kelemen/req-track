using System;

namespace ReqTrack.Domain.Core.Entities
{
    /// <summary>
    /// <c>Identity</c> is a value type which represents the unique identity of the entity.
    /// </summary>
    ///
    /// <remarks>
    /// Entities should always be compared by their identity, not by values. Blank identities always compare to false.
    /// </remarks>
    public struct Identity : IEquatable<Identity>
    {
        /// <summary>
        /// Identity for temporary and non-persisted objects.
        /// </summary>
        public static readonly Identity BlankIdentity = new Identity(string.Empty);

        /// <summary>
        /// Factory method for creating identity of objects from string.
        /// </summary>
        /// <param name="value">identity value</param>
        /// <returns>Unique entity identity.</returns>
        /// <remarks>Usage of this method isn't recommended.</remarks>
        public static Identity FromString(string value) => new Identity(value);

        private readonly string _id;

        private Identity(string id)
        {
            _id = id;
        }

        /// <summary>
        /// Unique identifer.
        /// </summary>
        public string Id => _id;

        /// <summary>
        /// Checks if the identity is blank.
        /// </summary>
        /// <returns><c>true</c> if the identity is blank identity.</returns>
        public bool IsBlankIdentity() => _id == string.Empty;

        public override bool Equals(object obj)
        {
            if (obj is Identity)
            {
                return Equals((Identity)obj);
            }
            return false;
        }

        public bool Equals(Identity other) => !IsBlankIdentity() && !other.IsBlankIdentity() && (_id == other._id);

        public override int GetHashCode() => _id.GetHashCode();

        public override string ToString() => _id;

        public static bool operator ==(Identity lhs, Identity rhs) => lhs.Equals(rhs);

        public static bool operator !=(Identity lhs, Identity rhs) => !(lhs == rhs);
    }
}
