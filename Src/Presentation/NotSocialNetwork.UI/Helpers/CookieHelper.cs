using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

        internal static async Task SetJwtHeader(HttpClient http, string token)
        {
            http.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue("bearer", token);
        }
    }
}
