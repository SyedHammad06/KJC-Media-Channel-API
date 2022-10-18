using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KJCMediaChannelWebAPI.Models
{
    public class AdminRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public long PhoneNo { get; set; }
        public string Password { get; set; }
        public string Department { get; set; }
    }
}
