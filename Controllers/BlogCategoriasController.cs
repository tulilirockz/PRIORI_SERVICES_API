using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Model;

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
        else
            return Ok(SelectedCategoria);
    }
}
