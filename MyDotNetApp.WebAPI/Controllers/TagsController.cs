using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDotNetApp.Models;

namespace MyDotNetApp.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TagsController : ControllerBase
{
    private readonly BlogContext _blogContext;

    public TagsController(BlogContext blogContext)
    {
        _blogContext = blogContext;
    }

    [HttpGet(Name = "GetTags")]
    public Task<List<TagDTO>> GetTags()
    {
        return _blogContext.Tags
            .Select(t => new TagDTO(t))
            .ToListAsync();
    }

    [HttpPost(Name = "CreateTag")]
    public async Task<ActionResult<TagDTO>> CreateTag(NewTagDTO newTag)
    {
        var tag = new Tag(newTag);
        _blogContext.Tags.Add(tag);
        await _blogContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTags), new { id = tag.Id }, new TagDTO(tag));
    }
}
