using Blazored.LocalStorage;
using System;
using System.Threading.Tasks;

namespace NotSocialNetwork.UI.Helpers
{
    /// <summary>
    /// Static class for working with cookies.
    /// </summary>
    /// <remarks>
    /// It is recommended to use this class for handling cookies for brevity and reliability.
    /// </remarks>
    internal static class CookieHelper
    {
        /// <summary>
        /// Modifies, or if it was empty, sets values for the <c>nightTheme</c> cookie.
        /// </summary>
        /// <param name="localStorage">A class containing methods for working with cookies.</param>
        /// <returns>
        /// New value for the <c> nightTheme </c> cookie.
        /// </returns>
        internal static async Task<bool> EditNightThemeCookies(ILocalStorageService localStorage)
        {
            bool oldValue = await GetNightThemeCookies(localStorage);

            await SetNightThemeCookies(localStorage, oldValue);

            if (oldValue)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Returns the value of the <c> nightTheme </c> cookie.
        /// </summary>
        /// <param name="localStorage">A class containing methods for working with cookies.</param>
        /// <returns></returns>
        internal static async Task<bool> GetNightThemeCookies(ILocalStorageService localStorage)
        {
            bool cookieResult = await localStorage.GetItemAsync<bool>("nightTheme");

            return cookieResult;
        }

        /// <summary>
        /// Configures a new value for the <c> nightTheme </c> cookie.
        /// </summary>
        /// <param name="localStorage">A class containing methods for working with cookies.</param>
        /// <param name="oldValue">
        /// The old value of the cookie, the opposite will be written to the file.
        /// </param>
        /// <returns></returns>
        private static async Task SetNightThemeCookies
            (ILocalStorageService localStorage, bool oldValue)
        {
            if (oldValue)
            {
                await localStorage.SetItemAsync("nightTheme", false);
            }
            else
            {
                await localStorage.SetItemAsync("nightTheme", true);
            }
        }

        /// <summary>
        /// Takes the value of the <c> token </c> cookie.
        /// </summary>
        /// <param name="localStorage">A class containing methods for working with cookies.</param>
        /// <returns></returns>
        internal static async Task<string> GetToken(ILocalStorageService localStorage)
        {
            string token = await localStorage.GetItemAsync<string>("token");

            return token;
        }

        /// <summary>
        /// Takes the value of the cookie <c> id </c> (user ID).
        /// </summary>
        /// <param name="localStorage">A class containing methods for working with cookies.</param>
        /// <returns></returns>
        internal static async Task<Guid> GetCurrentId(ILocalStorageService localStorage)
        {
            Guid id = await localStorage.GetItemAsync<Guid>("id");

            return id;
        }

        /// <summary>
        /// Retrieves the value of the <c> email </c> () cookie.
        /// </summary>
        /// <param name="localStorage">A class containing methods for working with cookies.</param>
        /// <returns></returns>
        internal static async Task<string> GetEmailAtCookies(ILocalStorageService localStorage)
        {
            string token = await localStorage.GetItemAsync<string>("email");

            return token;
        }

        /// <summary>
        /// Configures a new value for the <c>email</c> cookie.
        /// </summary>
        /// <param name="localStorage">A class containing methods for working with cookies.</param>
        /// <param name="email">New value for email cookie.</param>
        private static async void SetEmailAtCookies(ILocalStorageService localStorage, string email)
        {
            await localStorage.SetItemAsync<string>("email", email);
        }

        /// <summary>
        /// Configures new values for the <c> token </c> cookie.
        /// </summary>
        /// <param name="localStorage">A class containing methods for working with cookies.</param>
        /// <param name="token">New value for cookie token.</param>
        /// <returns></returns>
        private static async Task SetTokenAtCookiesAsync(ILocalStorageService localStorage, string token)
        {
            await localStorage.SetItemAsync<string>("token", token);
        }

        /// <summary>
        /// Configures new values for the <c> id </c> cookie.
        /// </summary>
        /// <param name="localStorage">A class containing methods for working with cookies.</param>
        /// <param name="id">New value for the cookie id.</param>
        /// <returns></returns>
        private static async Task SetIdAtCookiesAsync(ILocalStorageService localStorage, Guid id)
        {
            await localStorage.SetItemAsync<Guid>("id", id);
        }

        /// <summary>
        /// Configures new values for multiple cookies: <c>id</c>, <c>token</c> and <c>email</c>.
        /// </summary>
        /// <param name="localStorage">A class containing methods for working with cookies.</param>
        /// <param name="id">New value for the cookie id.</param>
        /// <param name="token">New value for cookie token.</param>
        /// <param name="email">New value for email cookie.</param>
        /// <returns></returns>
        internal static async Task<bool> SetNewUserCookies
            (ILocalStorageService localStorage, Guid id, string token, string email)
        {
            await SetIdAtCookiesAsync(localStorage, id);
            await SetTokenAtCookiesAsync(localStorage, token);
            SetEmailAtCookies(localStorage, email);

            return true;
        }
    }
}
