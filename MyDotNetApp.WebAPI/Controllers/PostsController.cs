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
            var tag = await _blogContext.Tags
                .AsTracking()
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

    [HttpPut(Name = "UpdatePost")]
    public async Task<ActionResult<PostDTO>> UpdatePost(UpdatePostDTO updatePost)
    {
        var post = await _blogContext.Posts
            .Include(p => p.Tags)
            .AsTracking()
            .SingleOrDefaultAsync(t => t.Id == updatePost.Id);

        if (post == null)
        {
            return NotFound();
        }

        post.Title = updatePost.Title;

        if (updatePost.TagIds != null)
        {
            var tagsToRemove = post.Tags.ToList();

            foreach (var tagToRemove in tagsToRemove)
            {
                post.Tags.Remove(tagToRemove);
            }

            var tagsToAdd = await _blogContext.Tags
                .Where(t => updatePost.TagIds.Contains(t.Id))
                .AsTracking()
                .ToListAsync();

            foreach (var tagToAdd in tagsToAdd)
            {
                post.Tags.Add(tagToAdd);
            }
        }

        await _blogContext.SaveChangesAsync();

        return NoContent();
    }
}
