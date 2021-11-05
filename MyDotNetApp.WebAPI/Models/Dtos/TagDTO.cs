using System.ComponentModel.DataAnnotations;

namespace MyDotNetApp.Models;

public class TagDTO
{
    public TagDTO(Tag tag)
    {
        Id = tag.Id;
        Name = tag.Name;
    }

    [Required]
    public long Id { get; set; }

    [Required]
    [MaxLength(64)]
    public string Name { get; set; }
}
