using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Model;
using Microsoft.AspNetCore.Authorization;
using PRIORI_SERVICES_API.Models.Dbos;

namespace PRIORI_SERVICES_API.Controllers;
[Route("api/Blog/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly PrioriDbContext _context;
    public CategoriasController(PrioriDbContext context)
    {
        _context = context;
    }

    [HttpGet(Name = "GetBlogCategorias")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<CategoriaBlog>>> GetCategorias()
    {
        return await _context.tblCategoriaBlog.ToListAsync();
    }

    [HttpGet("{id}", Name = "GetBlogCategoriasById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<CategoriaBlog>>> GetCategoriaFromID(int id)
    {
        CategoriaBlog? SelectedCategoria = await _context.tblCategoriaBlog.FindAsync(id);

        if (SelectedCategoria == null)
            return NotFound();

        return Ok(SelectedCategoria);
    }


    [HttpPost(Name = "CreateCategoria"), Authorize(Roles = "Consultor")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoriaBlog>> CreateBlogPost(CategoriaBlogDBO dbo)
    {
        var novaCategoria = new CategoriaBlog
        {
            nome_categoria = dbo.nome_categoria
        };

        _context.tblCategoriaBlog.Add(novaCategoria);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return BadRequest("Falha ao registrar mudanças no banco de dados");
        }

        return CreatedAtAction(
            nameof(GetCategorias),
            new
            {
                id_categoria = novaCategoria.id_categoria
            },
            novaCategoria.toDBO(novaCategoria));
    }

    [HttpDelete("{id}"), Authorize(Roles = "Consultor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCategoria(int id)
    {
        CategoriaBlog? SelectedCategoria = await _context.tblCategoriaBlog.FindAsync(id);

        if (SelectedCategoria == null)
            return NotFound();

        _context.tblCategoriaBlog.Remove(SelectedCategoria);

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
    public async Task<IActionResult> AlterCategoria(int id, CategoriaBlogDBO request)
    {
        CategoriaBlog? SelectedCategoria = await _context.tblCategoriaBlog.FindAsync(id);

        if (SelectedCategoria == null)
            return BadRequest();

        SelectedCategoria.nome_categoria = request.nome_categoria;

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