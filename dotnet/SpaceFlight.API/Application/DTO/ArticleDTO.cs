using SpaceFlight.API.Application.Model;

namespace SpaceFlight.API.Application.DTO.ViewModel
{
    public class ArticleDTO
    {
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

        public Article ToEntity()
        {
            return new()
            {
                Title = Title,
                Url = Url,
                NewsSite = NewsSite,
                Summary = Summary,
                PublishedAt = PublishedAt,
                UpdatedAt = UpdatedAt,
                Featured = Featured,
                ImageUrl = ImageUrl,
                Events = Events.Select(s => s.ToEntity()),
                Launches = Launches.Select(s => s.ToEntity())
            };
        }
    }

    public class LaunchesDTO
    {
        public string Id { get; set; }
        public string Provider { get; set; }

        public Launches ToEntity()
        {
            return new()
            {
                Id = Id,
                Provider = Provider,
            };
        }
    }

    public class EventsDTO
    {
        public string Id { get; set; }
        public string Provider { get; set; }

        public Events ToEntity()
        {
            return new()
            {
                Id = Id,
                Provider = Provider,
            };
        }
    }
}
