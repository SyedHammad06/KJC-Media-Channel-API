using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KJCMediaChannelWebAPI.Models
{
    public class Registration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public DateTime RegistrationTime { get; set; } = DateTime.Now;
        public string Regno { get; set; }
        public string Username { get; set; }
        public string Department { get; set; }
    }
}
