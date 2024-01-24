namespace JeanEdwardsMovieSite.Domain.Entities
{
    public class SearchQuery
    {
        public Guid Id { get; set; }
        public string? Query { get; set; }
        public DateTime? TimeStamp { get; set; }
    }
}
