using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDotNetApp.Models;

namespace MyDotNetApp.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly BlogContext _blogContext;

    public PostsController(BlogContext blogContext)
    {
        _blogContext = blogContext;
    }

    [HttpGet(Name = "GetPosts")]
    public Task<List<PostDTO>> GetPosts()
    {
        return _blogContext.Posts
            .Include(t => t.Tags)
            .Select(p => new PostDTO(p))
            .ToListAsync();
    }

    [HttpPost(Name = "CreatePost")]
    public async Task<ActionResult<PostDTO>> CreatePost(NewPostDTO newPost)
    {
        var post = new Post(newPost);

        foreach (var tagId in newPost.TagIds)
        {
            var tag = await _blogContext.Tags.AsTracking()
                .SingleOrDefaultAsync(t => t.Id == tagId);
            if (tag != null)
            {
                post.Tags.Add(tag);
            }
        }

        _blogContext.Posts.Add(post);

        await _blogContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPosts), new { id = post.Id }, new PostDTO(post));
    }
}
