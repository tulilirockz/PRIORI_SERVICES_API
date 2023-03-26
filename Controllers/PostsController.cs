using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Model;

namespace PRIORI_SERVICES_API.Controllers;
[Route("api/Blog/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly PrioriDbContext _context;
    public PostsController(PrioriDbContext context)
    {
        _context = context;
    }

    [HttpGet(Name = "GetBlogPosts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<PostBlog>>> GetPosts()
    {
        return await _context.tblPostBlog.ToListAsync();
    }

    [HttpGet("{id}", Name = "GetBlogPostById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<PostBlog>>> GetPostsFromID(int id)
    {
        PostBlog? SelectedBlogPost = await _context.tblPostBlog.FindAsync(id);

        if (SelectedBlogPost == null)
            return NotFound();
        else
            return Ok(SelectedBlogPost);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PostBlogDBO>> CreateBlogPost(PostBlogDBO dbo)
    {
        var novoPost = new PostBlog
        {
            id_autor = dbo.id_autor,
            id_categoria = dbo.id_categoria,
            titulo = dbo.titulo,
            data_criacao = System.TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")),
            conteudo = dbo.conteudo
        };

        _context.tblPostBlog.Add(novoPost);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return BadRequest();
        }

        return CreatedAtAction(
            nameof(GetPosts),
            new
            {
                id = novoPost.id_post
            },
            novoPost.toDBO(novoPost));
    }
}
