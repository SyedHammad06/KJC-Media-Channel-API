using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KJCMediaChannelWebAPI.Models
{
    public class News
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageLocation { get; set; } = null;
    }
}
