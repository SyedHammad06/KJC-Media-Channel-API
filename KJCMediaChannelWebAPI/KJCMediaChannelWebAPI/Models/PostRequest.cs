namespace KJCMediaChannelWebAPI.Models
{
    public class PostRequest
    {
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}
