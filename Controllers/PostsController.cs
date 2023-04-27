using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Model;
using Microsoft.AspNetCore.Authorization;
using PRIORI_SERVICES_API.Models.Dbos;
using PRIORI_SERVICES_API.Models;

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
    public async Task<ActionResult<IEnumerable<PostBlog>>> GetAll()
    {
        return await _context.tblPostBlog.ToListAsync();
    }

    [HttpGet("{id}", Name = "GetBlogPostById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<PostBlog>>> GetFromID(int id)
    {
        PostBlog? selected_blog_post = await _context.tblPostBlog.FindAsync(id);

        if (selected_blog_post == null)
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);

        return Ok(selected_blog_post);
    }

    [HttpPost(Name = "CreateBlogPost"), Authorize(Roles = "Consultor")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PostBlogDBO>> Create(PostBlogDBO dbo)
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
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);
        }

        return CreatedAtAction(
            nameof(GetAll),
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
    public async Task<IActionResult> Delete(int id)
    {
        PostBlog? selected_post = await _context.tblPostBlog.FindAsync(id);

        if (selected_post == null)
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);

        _context.tblPostBlog.Remove(selected_post);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);
        }

        return Ok();
    }

    [HttpPut("{id}"), Authorize(Roles = "Consultor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Alter(int id, PostBlogDBO BlogPost)
    {
        PostBlog? selected_blog_post = await _context.tblPostBlog.FindAsync(id);

        if (selected_blog_post == null)
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);

        selected_blog_post.conteudo = BlogPost.conteudo;
        selected_blog_post.data_criacao = System.TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
        selected_blog_post.descricao = BlogPost.descricao;
        selected_blog_post.id_autor = BlogPost.id_autor;
        selected_blog_post.id_categoria = BlogPost.id_categoria;
        selected_blog_post.titulo = BlogPost.titulo;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);
        }

        return Ok(selected_blog_post);
    }
}
