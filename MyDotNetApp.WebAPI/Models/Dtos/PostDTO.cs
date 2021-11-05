using System.ComponentModel.DataAnnotations;

namespace MyDotNetApp.Models;

public class PostDTO
{
    public PostDTO(Post post)
    {
        Id = post.Id;
        Title = post.Title;
        Tags = post.Tags.Select(t => new TagDTO(t)).ToList();
    }

    [Required]
    public long Id { get; set; }

    [Required]
    public string Title { get; set; }

    public List<TagDTO> Tags { get; set; } = new List<TagDTO>();
}
