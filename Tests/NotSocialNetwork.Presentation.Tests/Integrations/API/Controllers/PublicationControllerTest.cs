using Microsoft.AspNetCore.Mvc.Testing;
using NotSocialNetwork.API;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace NotSocialNetwork.Presentation.Tests.Integrations.API.Controllers
{
    public class PublicationControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        public PublicationControllerTest(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        private readonly HttpClient _client;

        [Theory]
        [InlineData("api/publication")]
        public async Task Get_GetJsonFile_JsonFile(string url)
        {
            var response = await _client.GetAsync(url + "/index=0");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
