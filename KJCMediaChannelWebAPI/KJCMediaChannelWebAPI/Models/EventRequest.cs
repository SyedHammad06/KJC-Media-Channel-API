namespace KJCMediaChannelWebAPI.Models
{
    public class EventRequest
    {
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public int MaxSlots { get; set; }
    }
}
