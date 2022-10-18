namespace KJCMediaChannelWebAPI.Models
{
    public class RegistrationRequest
    {
        public Guid EventId { get; set; }
        public string Regno { get; set; }
        public string Username { get; set; }
        public string Department { get; set; }
    }
}
