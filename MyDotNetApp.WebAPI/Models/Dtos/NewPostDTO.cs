using System.ComponentModel.DataAnnotations;

namespace MyDotNetApp.Models;

public class NewPostDTO
{
    [Required]
    public string Title { get; set; }

    [Required]
    public List<long> TagIds { get; set; } = new List<long>();
}
