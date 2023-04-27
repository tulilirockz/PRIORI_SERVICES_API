using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Models;
using PRIORI_SERVICES_API.Model;
using Microsoft.AspNetCore.Authorization;

namespace PRIORI_SERVICES_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarteiraInvestimentoController : ControllerBase
{
    private readonly PrioriDbContext _context;
    public CarteiraInvestimentoController(PrioriDbContext context) { _context = context; }

    [HttpGet("{id}", Name = "GetCarteiraById"), Authorize(Roles = "Consultor,Cliente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<CarteiraInvestimento>>> GetByID(int id)
    {
        CarteiraInvestimento? carteira = await _context.tblCarteiraInvestimentos.FindAsync(id);

        if (carteira == null)
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);

        return Ok(carteira);
    }

    [HttpPost(Name = "CreateCarteira"), Authorize(Roles = "Consultor,Cliente")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CarteiraInvestimentoDBO>> Create(CarteiraInvestimentoDBO dbo)
    {
        var carteira = new CarteiraInvestimento
        {
            id_efetuacao = dbo.id_efetuacao,
            id_cliente_carteira = dbo.id_cliente_carteira,
            id_investimento = dbo.id_investimento,
            rentabilidade_fixa = dbo.rentabilidade_fixa,
            rentabilidade_variavel = dbo.rentabilidade_variavel,
            data_efetuacao = dbo.data_efetuacao,
            data_encerramento = dbo.data_encerramento,
            saldo = dbo.saldo,
            status = dbo.status
        };

        _context.tblCarteiraInvestimentos.Add(carteira);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);
        }

        return CreatedAtAction(nameof(GetByID), new { id = carteira.id_efetuacao }, carteira.toDbo(carteira));
    }


    [HttpDelete("{id}"), Authorize(Roles = "Consultor,Cliente")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id, int clienteid)
    {
        CarteiraInvestimento? carteira = await _context.tblCarteiraInvestimentos.FindAsync(id);
        Cliente? cliente = await _context.tblClientes.FindAsync(clienteid);

        if (carteira == null || cliente == null)
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);

        carteira.status = "INATIVO";
        cliente.status = "INATIVO";

        try
        {
            _context.Update(carteira);
            _context.Update(cliente);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);
        }

        return Ok();
    }
}
