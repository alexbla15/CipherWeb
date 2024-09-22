﻿using CipherData.Requests;

namespace CipherData.Models
{
    /// <summary>
    /// Get user actions contract
    /// </summary>
    public class UserActionResponse
    {
        /// <summary>
        /// List of all user actions found
        /// </summary>
        [HebrewTranslation(nameof(UserActions))]
        public List<UserAction> UserActions { get; set; } = new();

        public static UserActionResponse Random()
        {
            return new UserActionResponse() { UserActions = RandomFuncs.FillRandomObjects(2, UserAction.Random)};
        }
    }
}
