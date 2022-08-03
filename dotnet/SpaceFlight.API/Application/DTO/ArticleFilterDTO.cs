namespace SpaceFlight.API.Application.DTO
{
    public class ArticleFilterDTO
    {
        public ArticleFilterDTO(int limit)
        {
            Limit = limit;
        }

        public ArticleFilterDTO(int limit, int skip)
        {
            Limit = limit;
            Skip = skip;
        }

        public OrderType Order { get; set; } = OrderType.Id;
        public int Limit { get; private set; }
        public int Skip { get; private set; } = 0;
        public string GreaterThan { get; private set; } = null;

        public void AddGreaterThan(ArticleField field, string value) => GreaterThan = $"{field.ToString().ToLower()}_gt={value}";
    }

    public enum OrderType
    {
        Id
    }

    public enum ArticleField
    {
        Id,
        Title,
        Url,
        ImageUrl,
        NewsSite,
        Summary,
        PublishedAt,
        UpdatedAt,
        Featured
    }
}
