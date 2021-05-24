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

        internal static async Task<string> GetToken(ILocalStorageService localStorage)
        {
            string token = await localStorage.GetItemAsync<string>("token");

            return token;
        }

        internal static async Task<Guid> GetCurrentId(ILocalStorageService localStorage)
        {
            Guid id = await localStorage.GetItemAsync<Guid>("id");

            return id;
        }

        internal static async Task<string> GetEmailAtCookies(ILocalStorageService localStorage)
        {
            string token = await localStorage.GetItemAsync<string>("email");

            return token;
        }

        private static async void SetEmailAtCookies(ILocalStorageService localStorage, string email)
        {
            await localStorage.SetItemAsync<string>("email", email);
        }

        private static async Task SetTokenAtCookiesAsync(ILocalStorageService localStorage, string token)
        {
            await localStorage.SetItemAsync<string>("token", token);
        }

        private static async Task SetIdAtCookiesAsync(ILocalStorageService localStorage, Guid id)
        {
            await localStorage.SetItemAsync<Guid>("id", id);
        }

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
