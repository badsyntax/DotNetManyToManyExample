
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDotNetApp.Models
{
    [Table("Posts")]
    public class Post
    {
        public Post(NewPostDTO newPost)
        {
            Title = newPost.Title;
        }

        [Required]
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

        public List<Tag> Tags { get; } = new List<Tag>();

        public List<PostTag> PostTags { get; } = new List<PostTag>();
    }
}
