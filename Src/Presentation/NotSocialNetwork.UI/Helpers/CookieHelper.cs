using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotSocialNetwork.UI.Helpers
{
    internal static class CookieHelper
    {
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

        internal static async Task<bool> GetNightThemeCookies(ILocalStorageService localStorage)
        {
            bool cookieResult = await localStorage.GetItemAsync<bool>("nightTheme");

            return cookieResult;
        }

        internal static async Task SetNightThemeCookies
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
    }
}
