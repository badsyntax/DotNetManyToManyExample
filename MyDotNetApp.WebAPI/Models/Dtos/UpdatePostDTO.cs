using System.ComponentModel.DataAnnotations;

namespace MyDotNetApp.Models;

public class UpdatePostDTO
{
    [Required]
    public long Id { get; set; }

    [Required]
    public string Title { get; set; }

    public List<long>? TagIds { get; set; }
}
