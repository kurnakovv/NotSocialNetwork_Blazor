using NotSocialNetwork.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NotSocialNetwork.UI.Helpers
{
    public class HttpHelper
    {
        internal const string APIAddress = "https://localhost:44353/api/";


        internal static void SetJwtHeader(HttpClient http, string token)
        {
            http.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue("bearer", token);
        }

        internal static async Task<UserDTO> GetUserAsync(HttpClient http, Guid userId, string token = "")
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                token = CurrentUserData.Token;
            }
            SetJwtHeader(http, token);
            return await http.GetFromJsonAsync<UserDTO>($"{APIAddress}user/{userId}");
        }
    }
}
