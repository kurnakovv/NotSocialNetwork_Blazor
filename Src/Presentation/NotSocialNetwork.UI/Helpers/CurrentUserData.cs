using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotSocialNetwork.UI.Helpers
{
    public static class CurrentUserData
    {
        public static Guid Id { get; set; }
        public static string Token { get; set; }
        public static string Email { get; set; }

        public static bool IsNightTheme { get; set; }
    }
}
