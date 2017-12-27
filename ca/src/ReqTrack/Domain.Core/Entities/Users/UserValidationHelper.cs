using System;
using System.Linq;
using System.Text;

namespace ReqTrack.Domain.Core.Entities.Users
{
    /// <summary>
    /// Helper class for common operations and constants related to the user.
    /// </summary>
    public static class UserValidationHelper
    {        
        /// <summary>
        /// Maximum length of the user's userName.
        /// </summary>
        public static readonly int MaximumUserNameLength = 50;

        public static readonly int MaximumDisplayNameLength = 100;

        /// <summary>
        /// Calculate the hash of the password.
        /// </summary>
        /// <param name="password">Password to hash.</param>
        /// <returns>Hashed password using the MD5 algorithm.</returns>
        public static string HashPassword(string password)
        {
            var hasher = System.Security.Cryptography.MD5.Create();
            var input = Encoding.UTF8.GetBytes(password);
            var output = hasher.ComputeHash(input);

            return output.Aggregate(String.Empty, (current, o) => current + o.ToString("x2"));
        }

        /// <summary>
        /// Checks if the passed user name represents a valid user name.
        /// </summary>
        /// <param name="userName">Username to be checked.</param>
        /// <returns><c>true</c> if the user name is valid.</returns>
        public static bool IsUserNameValid(string userName)
        {
            return !String.IsNullOrWhiteSpace(userName) && userName.Length <= MaximumUserNameLength;
        }

        /// <summary>
        /// Checks if the passed display name represents a valid display name.
        /// </summary>
        /// <param name="displayName">Display name to be checked.</param>
        /// <returns><c>true</c> if the display name is valid.</returns>
        public static bool IsDisplayNameValid(string displayName)
        {
            return !string.IsNullOrWhiteSpace(displayName) && displayName.Length <= MaximumDisplayNameLength;
        }

        /// <summary>
        /// Checks if the password hash is valid.
        /// </summary>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public static bool IsPasswordHashValid(string passwordHash)
        {
            return !string.IsNullOrWhiteSpace(passwordHash) && passwordHash.Length == 32;
        }
    }
}