namespace ReqTrack.Domain.Core.Entities.Interfaces
{
    /// <summary>
    /// <c>IEntity</c> is a interface which all entities implement.
    /// </summary>
    /// 
    /// <typeparam name="T">Entity type</typeparam>
    public interface IEntity<T> where T : IEntity<T>
    {
        /// <summary>
        /// Identity of the entity.
        /// </summary>
        Identity Id { get; }

        /// <summary>
        /// Checks if a entity has blank identity.
        /// </summary>
        /// <returns><c>true</c> if a entity has blank identity.</returns>
        bool HasBlankIdentity();

        /// <summary>
        /// Checks if entities have same identity.
        /// </summary>
        /// <param name="other">other entity.</param>
        /// <returns><c>true</c> if entities have same identity.</returns>
        bool HasSameIdentityAs(T other);

        /// <summary>
        /// Checks if entities have same value, not including the identity.
        /// </summary>
        /// <param name="other">other entity.</param>
        /// <returns><c>true</c> if entities have same value.</returns>
        bool HasSameValueAs(T other);
    }
}
