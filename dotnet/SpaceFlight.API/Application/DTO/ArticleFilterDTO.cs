namespace SpaceFlight.API.Application.DTO
{
    public class ArticleFilterDTO
    {
        public ArticleFilterDTO(int limit, int skip)
        {
            Limit = limit;
            Skip = skip;
        }

        public OrderType Order { get; set; } = OrderType.Id;
        public int Limit { get; private set; }
        public int Skip { get; private set; }
    }

    public enum OrderType
    {
        Id
    }
}
