using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDotNetApp.Models
{
    [Table("PostTags")]
    public class PostTag
    {
        [Required]
        public long PostId { get; set; }

        [Required]
        public Post Post { get; set; }

        [Required]
        public long TagId { get; set; }

        [Required]
        public Tag Tag { get; set; }
    }
}
