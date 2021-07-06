using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotSocialNetwork.WebShared.Helpers
{

    /// <summary>
    /// Contains user data used during the session.
    /// </summary>
    /// <remarks>
    /// It is recommended to use this class for storing and retrieving user data
    /// during session instead of persistent fetching from cookies.
    /// However, it is recommended to change the data on a long-term basis through cookies.
    /// </remarks>
    public static class CurrentUserData
    {
        public static Guid Id { get; set; }
        public static string Token { get; set; }
        public static string Email { get; set; }

        public static bool IsAuthorized { get; set; }
        public static bool IsNightTheme { get; set; }

        /// <summary>
        /// Configures new global values for user data.
        /// </summary>
        /// <param name="newID"></param>
        /// <param name="newToken"></param>
        /// <param name="newEmail"></param>
        public static void SetNewData(Guid newID, string newToken, string newEmail)
        {
            Id = newID;
            Token = newToken;
            Email = newEmail;
        }
    }
}
