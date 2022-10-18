using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KJCMediaChannelWebAPI.Models
{
    public class CommentRequest
    {
        public Guid PostId { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
    }
}
