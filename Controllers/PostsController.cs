using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Model;
using Microsoft.AspNetCore.Authorization;
using PRIORI_SERVICES_API.Models.Dbos;

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

        return Ok(SelectedBlogPost);
    }

    [HttpPost(Name = "CreateBlogPost"), Authorize(Roles = "Consultor")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PostBlogDbo>> CreateBlogPost(PostBlogDbo dbo)
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
            return BadRequest("Falha ao registrar mudanças no banco de dados");
        }

        return CreatedAtAction(
            nameof(GetPosts),
            new
            {
                id = novoPost.id_post
            },
            novoPost.toDBO(novoPost));
    }

    [HttpDelete("{id}"), Authorize(Roles = "Consultor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteBlogPost(int id)
    {
        PostBlog? SelectedBlogPost = await _context.tblPostBlog.FindAsync(id);

        if (SelectedBlogPost == null)
            return NotFound();

        _context.tblPostBlog.Remove(SelectedBlogPost);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return BadRequest("Falha ao registrar mudanças no banco de dados");
        }

        return NoContent();
    }

    [HttpPut("{id}"), Authorize(Roles = "Consultor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AlterBlogPost(int id, PostBlogDbo BlogPost)
    {
        PostBlog? SelectedBlogPost = await _context.tblPostBlog.FindAsync(id);

        if (SelectedBlogPost == null)
            return BadRequest();

        SelectedBlogPost.conteudo = BlogPost.conteudo;
        SelectedBlogPost.data_criacao = System.TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
        SelectedBlogPost.descricao = BlogPost.descricao;
        SelectedBlogPost.id_autor = BlogPost.id_autor;
        SelectedBlogPost.id_categoria = BlogPost.id_categoria;
        SelectedBlogPost.titulo = BlogPost.titulo;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return BadRequest();
        }

        return NoContent();
    }
}
