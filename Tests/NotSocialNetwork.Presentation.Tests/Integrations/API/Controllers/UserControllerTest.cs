using Microsoft.AspNetCore.Mvc.Testing;
using NotSocialNetwork.API;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace NotSocialNetwork.Presentation.Tests.Integrations.API.Controllers
{
    public class UserControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        public UserControllerTest(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        private readonly HttpClient _client;

        // TODO: Add auth token for client.
        //[Theory]
        //[InlineData("api/user")]
        //public async Task Get_GetJsonFile_JsonFile(string url)
        //{
        //    var response = await _client.GetAsync(url);

        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //    Assert.Equal("application/json; charset=utf-8",
        //        response.Content.Headers.ContentType.ToString());
        //}
    }
}
