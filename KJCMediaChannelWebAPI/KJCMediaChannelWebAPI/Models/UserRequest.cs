namespace KJCMediaChannelWebAPI.Models
{
    public class UserRequest
    {
        public string RegNo { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public long PhoneNo { get; set; }
        public string Password { get; set; }
        public string Department { get; set; }
    }
}
