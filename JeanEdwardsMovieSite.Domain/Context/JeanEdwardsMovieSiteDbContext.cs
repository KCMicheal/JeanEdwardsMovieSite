using JeanEdwardsMovieSite.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JeanEdwardsMovieSite.Domain.Context
{
    public class JeanEdwardsMovieSiteDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public JeanEdwardsMovieSiteDbContext(DbContextOptions<JeanEdwardsMovieSiteDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<SearchQuery> SearchQueries { get; set; }
    }
}
