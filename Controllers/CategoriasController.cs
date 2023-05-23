using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Model;
using Microsoft.AspNetCore.Authorization;
using PRIORI_SERVICES_API.Model.DBO;
using PRIORI_SERVICES_API.Shared;

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


    [HttpGet(Name = "GetCategorias")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<CategoriaBlog>>> GetAll()
    {
        return await _context.tblCategoriaBlog.ToListAsync();
    }

    [HttpGet("{id}", Name = "GetCategoriaById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<CategoriaBlog>>> GetFromID(int id)
    {
        CategoriaBlog? selected_categoria = await _context.tblCategoriaBlog.FindAsync(id);

        if (selected_categoria == null)
            return BadRequest(DefaultRequests.BAD_REQUEST);

        return Ok(selected_categoria);
    }

    [HttpPost(Name = "CreateCategoria"), Authorize(Roles = "Consultor")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoriaBlog>> Create(CategoriaBlogDBO dbo)
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
        catch (Exception e) when (e is DbUpdateConcurrencyException || e is DbUpdateException)
        {
            return BadRequest(DefaultRequests.BAD_REQUEST);
        }

        return CreatedAtAction(
            nameof(GetAll),
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
    public async Task<IActionResult> Delete(int id)
    {
        CategoriaBlog? selected_categoria = await _context.tblCategoriaBlog.FindAsync(id);

        if (selected_categoria == null)
            return BadRequest(DefaultRequests.BAD_REQUEST);

        try
        {

            _context.tblCategoriaBlog.Remove(selected_categoria);
            await _context.SaveChangesAsync();
        }
        catch (Exception e) when (e is DbUpdateConcurrencyException || e is DbUpdateException)
        {
            return BadRequest(DefaultRequests.BAD_REQUEST);
        }

        return Ok();
    }

    [HttpPut("{id}"), Authorize(Roles = "Consultor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Alter(int id, CategoriaBlogDBO request)
    {
        CategoriaBlog? selected_categoria = await _context.tblCategoriaBlog.FindAsync(id);

        if (selected_categoria == null)
            return BadRequest(DefaultRequests.BAD_REQUEST);

        selected_categoria.nome_categoria = request.nome_categoria;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e) when (e is DbUpdateConcurrencyException || e is DbUpdateException)
        {
            return BadRequest(DefaultRequests.BAD_REQUEST);
        }

        return Ok(selected_categoria);
    }
}