namespace KJCMediaChannelWebAPI.Models
{
    public class NewsRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageLocation { get; set; } = null;
    }
}
