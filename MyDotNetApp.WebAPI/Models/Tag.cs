using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDotNetApp.Models
{
    [Table("Tags")]
    public class Tag
    {
        public Tag()
        {

        }

        public Tag(NewTagDTO newTag)
        {
            Name = newTag.Name;
        }

        [Required]
        public long Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        public List<Post> Posts { get; } = new List<Post>();

        public List<PostTag> PostTags { get; } = new List<PostTag>();
    }
}
