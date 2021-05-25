using System;

namespace NotSocialNetwork.UI.Helpers
{
    /// <summary>
    /// Contains user data used during the session.
    /// </summary>
    /// <remarks>
    /// It is recommended to use this class for storing and retrieving user data
    /// during session instead of persistent fetching from cookies.
    /// However, it is recommended to change the data on a long-term basis through cookies.
    /// </remarks>
    internal static class CurrentUserData
    {
        internal static Guid Id { get; set; }
        internal static string Token { get; set; }
        internal static string Email { get; set; }

        internal static bool IsAuthorized { get; set; }
        internal static bool IsNightTheme { get; set; }

        /// <summary>
        /// Configures new global values for user data.
        /// </summary>
        /// <param name="newID"></param>
        /// <param name="newToken"></param>
        /// <param name="newEmail"></param>
        internal static void SetNewData(Guid newID, string newToken, string newEmail)
        {
            Id = newID;
            Token = newToken;
            Email = newEmail;
        }
    }
}
