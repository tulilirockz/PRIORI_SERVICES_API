using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Models;
using PRIORI_SERVICES_API.Models.Dbos;

namespace PRIORI_SERVICES_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CarteiraInvestimentoController : ControllerBase
{
    private readonly PrioriDbContext _context;
    public CarteiraInvestimentoController(PrioriDbContext context)
    {
        _context = context;
    }

    [HttpGet(Name = "GetCarteiras")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<CarteiraInvestimento>>> GetCarteira()
    {
        return await _context.tblCarteiraInvestimentos.ToListAsync();
    }

    [HttpGet("{id}", Name = "GetCarteiraById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<CarteiraInvestimento>>> GetCarteiraFromID(int id)
    {
        CarteiraInvestimento? carteira = await _context.tblCarteiraInvestimentos.FindAsync(id);

        if (carteira == null)
            return NotFound();

        return Ok(carteira);
    }

    [HttpPost(Name = "CreateCarteira")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CarteiraInvestimentoDbo>> CreateaCarteiraPost(CarteiraInvestimentoDbo dbo)
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
            return BadRequest("Falha ao registrar mudanças no banco de dados");
        }

        return CreatedAtAction(
            nameof(GetCarteira),
        new
        {
            id = carteira.id_efetuacao
        },
            carteira.toDbo(carteira));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCarteira(int id)
    {
        CarteiraInvestimento? carteira = await _context.tblCarteiraInvestimentos.FindAsync(id);

        if (carteira == null)
            return NotFound();

        _context.tblCarteiraInvestimentos.Remove(carteira);

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

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AlterCarteira(int id, CarteiraInvestimentoDbo carteiraDbo)
    {
        CarteiraInvestimento? carteira = await _context.tblCarteiraInvestimentos.FindAsync(id);

        if (carteira == null)
            return BadRequest();

        carteira.id_efetuacao = carteiraDbo.id_efetuacao;
        carteira.id_cliente_carteira = carteiraDbo.id_cliente_carteira;
        carteira.id_investimento = carteiraDbo.id_investimento;
        carteira.data_efetuacao = carteiraDbo.data_efetuacao;
        carteira.data_encerramento = carteiraDbo.data_encerramento;
        carteira.rentabilidade_fixa = carteiraDbo.rentabilidade_fixa;
        carteira.rentabilidade_variavel = carteiraDbo.rentabilidade_variavel;
        carteira.saldo = carteiraDbo.saldo;
        carteira.status = carteiraDbo.status;


        try
        {
            _context.Update(carteira);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return BadRequest();
        }

        return NoContent();
    }
}
