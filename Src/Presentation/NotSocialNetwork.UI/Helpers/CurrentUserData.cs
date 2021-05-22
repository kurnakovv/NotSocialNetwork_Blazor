using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotSocialNetwork.UI.Helpers
{
    internal static class CurrentUserData
    {
        internal static Guid Id { get; set; }
        internal static string Token { get; set; }
        internal static string Email { get; set; }

        internal static bool IsNightTheme { get; set; }

        internal static void SetNewData(Guid newID, string newToken, string newEmail)
        {
            Id = newID;
            Token = newToken;
            Email = newEmail;
        }
    }
}
