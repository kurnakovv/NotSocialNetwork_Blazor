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

        internal static async void SetEmailAtCookies(ILocalStorageService localStorage, string email)
        {
            await localStorage.SetItemAsync<string>("email", email);
        }

        internal static async Task SetTokenAtCookiesAsync(ILocalStorageService localStorage, string token)
        {
            await localStorage.SetItemAsync<string>("token", token);
        }

        internal static async Task SetIdAtCookiesAsync(ILocalStorageService localStorage, Guid id)
        {
            await localStorage.SetItemAsync<Guid>("id", id);
        }
    }
}
