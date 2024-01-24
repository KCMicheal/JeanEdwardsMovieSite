using JeanEdwardsMovieSite.Domain.Context;
using JeanEdwardsMovieSite.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace JeanEdwardsMovieSite_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JeanEdwardsMovieSiteController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly JeanEdwardsMovieSiteDbContext _dbContext;
        private readonly IConfiguration _config;

        public JeanEdwardsMovieSiteController(JeanEdwardsMovieSiteDbContext dbContext, IConfiguration configuration)
        {
            _client = new HttpClient();
            _dbContext = dbContext;
            _config = configuration;
        }

        [HttpGet("{title}")]
        public async Task<IActionResult> GetMovieByTitle(string title)
        {
            var response = await _client.GetAsync($"http://www.omdbapi.com/?apikey={_config["OMDbApi:ApiKey"]}&t={title}");
            var content = await response.Content.ReadAsStringAsync();
            Film? movie = JsonConvert.DeserializeObject<Film>(content);

            var searchQuery = new SearchQuery
            {
                Query = title,
                TimeStamp = DateTime.Now
            };
            _dbContext.SearchQueries.Add(searchQuery);
            await _dbContext.SaveChangesAsync();

            return Ok(movie);
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestSearchQueries()
        {
            List<SearchQuery> searchQueries = await _dbContext.SearchQueries.ToListAsync();
            return Ok(searchQueries);
        }
    }
}
