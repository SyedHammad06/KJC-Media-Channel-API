using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KJCMediaChannelWebAPI.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string RegNo { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public long PhoneNo { get; set; }
        public string Password { get; set; }
        public string Department { get; set; }
        public bool MakePost { get; set; } = false;
    }
}
