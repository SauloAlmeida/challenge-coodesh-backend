namespace SpaceFlight.API.Application.DTO.ViewModel
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string NewsSite { get; set; }
        public string Summary { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Featured { get; set; }
        public IList<LaunchesDTO> Launches { get; set; }
        public IList<EventsDTO> Events { get; set; }
    }

    public class LaunchesDTO
    {
        public string Id { get; set; }
        public string Provider { get; set; }
    }

    public class EventsDTO
    {
        public string Id { get; set; }
        public string Provider { get; set; }
    }

}
