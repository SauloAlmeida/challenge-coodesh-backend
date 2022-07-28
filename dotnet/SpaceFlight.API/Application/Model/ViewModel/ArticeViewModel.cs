namespace SpaceFlight.API.Application.Model.ViewModel
{
    public class ArticeViewModel
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
        public IList<LaunchesViewModel> Launches { get; set; }
        public IList<EventsViewModel> Events { get; set; }
    }

    public class LaunchesViewModel
    {
        public string Id { get; set; }
        public string Provider { get; set; }
    }

    public class EventsViewModel
    {
        public string Id { get; set; }
        public string Provider { get; set; }
    }

}
