using ReqTrack.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReqTrack.Domain.UseCases.Core.Boundary.Objects.Extensions
{
    public static class IdentityExtensions
    {
        public static string ToBoundaryIdentity(this Identity id)
        {
            return id.ToString();
        }

        /// <summary>
        /// Converts boundary identity to domain identity.
        /// </summary>
        /// <param name="id">Boundary identity.</param>
        /// <param name="identityIfNull">Alternate identity if <paramref name="id"/> is <c>null</c>.</param>
        /// <returns>Domain identity.</returns>
        public static Identity ToDomainIdentity(this string id, string identityIfNull = null)
        {
            return string.IsNullOrEmpty(id) 
                ? (string.IsNullOrEmpty(identityIfNull) ? Identity.BlankIdentity : Identity.FromString(identityIfNull)) 
                : Identity.FromString(id);
        }
    }
}
