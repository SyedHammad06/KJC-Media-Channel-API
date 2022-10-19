using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KJCMediaChannelWebAPI.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? ImageLocation { get; set; } = null;
    }
}
