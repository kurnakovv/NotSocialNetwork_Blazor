using NotSocialNetwork.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NotSocialNetwork.WebShared.Helpers
{
    /// <summary>
    /// Contains methods and variables for working with HttpClient.
    /// </summary>
    /// <remarks>
    /// It is recommended to use this class for API operations for brevity and reliability.
    /// </remarks>
    public class HttpHelper
    {
        public const string API_ADDRESS = "https://localhost:44353/";

        /// <summary>
        /// Configures the authorization header value for the HTTP request.
        /// </summary>
        /// <param name="http">Object of HTTP request.</param>
        /// <param name="token">The token to be set in the authorization header.</param>
        public static void SetJwtHeader(HttpClient http, string token)
        {
            http.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        /// <summary>
        /// Searches for a user via the API and returns it.
        /// </summary>
        /// <param name="http">Object of HTTP request.</param>
        /// <param name="userId">The identifier of the searched for user.</param>
        /// <param name="token">
        /// User authentication token.
        /// If not specified, the token stored in <c>CurrentUserData.Token</c> will be used.</param>
        /// <returns>
        /// If the user was found, it returns his data as an instance of the <c> UserDTO </c> class.
        /// Otherwise, an exception will be returned.
        /// </returns>
        public static async Task<UserDTO> GetUserAsync(HttpClient http, Guid userId, string token = "")
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                token = CurrentUserData.Token;
            }

            SetJwtHeader(http, token);
            return await http.GetFromJsonAsync<UserDTO>($"{API_ADDRESS}api/user/{userId}");
        }
    }
}
