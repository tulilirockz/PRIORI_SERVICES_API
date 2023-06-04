using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Model;
using PRIORI_SERVICES_API.Shared;
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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CarteiraInvestimento>>> GetByID(int id)
    {
        CarteiraInvestimento? carteira = await _context.tblCarteiraInvestimentos.FindAsync(id);

        if (carteira == null)
            return BadRequest(DefaultRequests.BAD_REQUEST);

        return Ok(carteira);
    }

    [HttpGet("ByUserID/{id}", Name = "GetCarteiraByUserID"), Authorize(Roles = "Consultor,Cliente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CarteiraInvestimento>>> GetByUserID(int id)
    {
        if (!await _context.tblClientes.AnyAsync(e => e.id_cliente == id))
            return BadRequest(DefaultRequests.BAD_REQUEST);

        try
        {
            return await (from carteiras_user
                          in _context.tblCarteiraInvestimentos
                          where carteiras_user.id_cliente_carteira == id
                          select carteiras_user).ToListAsync();
        }
        catch (Exception e) when (e is ArgumentNullException || e is OperationCanceledException)
        {
            return BadRequest(DefaultRequests.BAD_REQUEST);
        }
    }

    [HttpPost(Name = "CreateCarteira"), Authorize(Roles = "Consultor,Cliente")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CarteiraInvestimentoDBO>> Create(CarteiraInvestimentoDBO dbo)
    {
        var carteira = new CarteiraInvestimento
        {
            id_cliente_carteira = dbo.id_cliente_carteira,
            id_investimento = dbo.id_investimento,
            rentabilidade_fixa = dbo.rentabilidade_fixa,
            rentabilidade_variavel = dbo.rentabilidade_variavel,
            data_encerramento = dbo.data_encerramento,
            saldo = dbo.saldo,
            status = dbo.status,
            valor_aplicado = dbo.valor_aplicado
        };

        _context.tblCarteiraInvestimentos.Add(carteira);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e) when (e is DbUpdateConcurrencyException || e is DbUpdateException)
        {
            return BadRequest(DefaultRequests.BAD_REQUEST);
        }

        return CreatedAtAction(nameof(GetByID), new { id = carteira.id_efetuacao }, carteira.toDBO());
    }


    [HttpDelete("{id}"), Authorize(Roles = "Consultor,Cliente")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        CarteiraInvestimento? carteira = await _context.tblCarteiraInvestimentos.FindAsync(id);

        if (carteira == null)
            return BadRequest(DefaultRequests.BAD_REQUEST);

        Cliente? cliente = await _context.tblClientes.FindAsync(carteira.id_cliente_carteira);

        if (cliente == null)
        {
            return BadRequest(DefaultRequests.BAD_REQUEST);
        }

        carteira.status = "INATIVO";
        cliente.status = "INATIVO";

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e) when (e is DbUpdateConcurrencyException || e is DbUpdateException)
        {
            return BadRequest(DefaultRequests.BAD_REQUEST);
        }

        return Ok();
    }

    [HttpGet("saldo/{id}", Name = "SaldoCliente"), Authorize(Roles = "Cliente,Consultor")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<decimal>> SaldoUsuario(int id) {

        DateTime? carteiramax_data = await (
            from carteiras_user 
            in _context.tblCarteiraInvestimentos
            where carteiras_user.id_cliente_carteira == id
            select carteiras_user.data_efetuacao).MaxAsync();

        if (carteiramax_data == null)
            return BadRequest(DefaultRequests.BAD_REQUEST);

        decimal? saldo_mais_recente = await (
            from carteiras_user 
            in _context.tblCarteiraInvestimentos 
            where carteiras_user.data_efetuacao == carteiramax_data && carteiras_user.id_cliente_carteira == id
            select carteiras_user.saldo).SingleAsync();

        if (saldo_mais_recente == null)
            return BadRequest(DefaultRequests.BAD_REQUEST);

        return Ok(saldo_mais_recente);
    }

    [HttpPut("saldo/{id}", Name = "SaldoCliente"), Authorize(Roles = "Cliente,Consultor")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AlterarSaldo(int id, decimal saldo_aplicado) {
        DateTime? carteiramax_data = await (
            from carteiras_user 
            in _context.tblCarteiraInvestimentos
            where carteiras_user.id_cliente_carteira == id
            select carteiras_user.data_efetuacao).MaxAsync();

        if (carteiramax_data == null)
            return BadRequest(DefaultRequests.BAD_REQUEST);

        CarteiraInvestimento? saldo_mais_recente = await (
            from carteiras_user 
            in _context.tblCarteiraInvestimentos 
            where carteiras_user.data_efetuacao == carteiramax_data && carteiras_user.id_cliente_carteira == id
            select carteiras_user).SingleAsync();

        if (saldo_mais_recente == null)
            return BadRequest(DefaultRequests.BAD_REQUEST);

        saldo_mais_recente.saldo = saldo_aplicado;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e) when (e is DbUpdateConcurrencyException || e is DbUpdateException)
        {
            return BadRequest(DefaultRequests.BAD_REQUEST);
        }

        return Ok();
    }
}
