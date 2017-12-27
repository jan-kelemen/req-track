using System;
using System.Collections.Generic;
using System.Text;

namespace ReqTrack.Domain.Core.Entities.Users
{
    public class BasicUser : Entity<BasicUser>
    {
        private string _displayName;

        public BasicUser(Identity id, string displayName) : base(id)
        {
            DisplayName = displayName;
        }

        /// <summary>
        /// Display name of the user, can't be longer than <see cref="UserValidationHelper.MaximumDisplayNameLength"/> characters.
        /// </summary>
        /// <exception cref="ArgumentException">If passed display name is invalid. <see cref="UserValidationHelper.IsDisplayNameValid"/></exception>
        public string DisplayName
        {
            get => _displayName;
            set
            {
                if (!UserValidationHelper.IsDisplayNameValid(value))
                {
                    throw new ArgumentException($"Display name \"{value}\" is invalid.");
                }

                _displayName = value;
            }
        }
    }
}
