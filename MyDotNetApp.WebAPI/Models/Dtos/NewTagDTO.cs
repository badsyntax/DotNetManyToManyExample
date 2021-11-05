using System.ComponentModel.DataAnnotations;

namespace MyDotNetApp.Models;

public class NewTagDTO
{
    [Required]
    [MaxLength(64)]
    public string Name { get; set; }
}
