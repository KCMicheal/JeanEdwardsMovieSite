using Xunit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using JeanEdwardsMovieSite.Domain.Entities;
using JeanEdwardsMovieSite_API;
using Microsoft.Extensions.Hosting;

namespace JeanEdwardsMovieSiteUnitTests
{
    public class JeanEdwardsMovieSiteControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public JeanEdwardsMovieSiteControllerTests()
        {
            var builder = WebApplication.CreateBuilder();
            builder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseTestServer();
            });

            var app = builder.Build();
            _server = app.Start();
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task GetMovieByTitle_ReturnsOkResult_WithValidTitle()
        {
            // Arrange
            var title = "Some Movie Title";

            // Act
            var response = await _client.GetAsync($"/api/JeanEdwardsMovieSite/{title}");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());

            var content = await response.Content.ReadAsStringAsync();
            var film = JsonSerializer.Deserialize<Film>(content);
            Assert.NotNull(film);
            Assert.Equal(title, film.Title);
        }

        [Fact]
        public async Task GetLatestSearchQueries_ReturnsOkResult()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/api/JeanEdwardsMovieSite/latest");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());

            var content = await response.Content.ReadAsStringAsync();
            var searchQueries = JsonSerializer.Deserialize<List<SearchQuery>>(content);
            Assert.NotEmpty(searchQueries);
        }
    }
}