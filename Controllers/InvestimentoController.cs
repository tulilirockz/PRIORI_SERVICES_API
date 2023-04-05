using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Models.Dbos;
using PRIORI_SERVICES_API.Models;

namespace PRIORI_SERVICES_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestimentoController : ControllerBase
    {
        private readonly PrioriDbContext _context;
        public InvestimentoController(PrioriDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetInvestimentos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Investimento>>> GetInvestimentos()
        {
            return await _context.tblInvestimentos.ToListAsync();
        }

        [HttpGet("{id}", Name = "GetInvestimentoById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Investimento>>> GetInvestimentoFromID(int id)
        {
            Investimento? investimento = await _context.tblInvestimentos.FindAsync(id);

            if (investimento == null)
                return NotFound();

            return Ok(investimento);
        }

        [HttpPost(Name = "CreateInvestimento")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InvestimentoDbo>> CreateaInvestimento(InvestimentoDbo dbo)
        {
            var investimento = new Investimento
            {
                id_investimento = dbo.id_investimento,
                id_riscoInvestimento = dbo.id_riscoInvestimento,
                nome = dbo.nome,
                tipo_investimento = dbo.tipo_investimento,
                rentabilidade_fixa = dbo.rentabilidade_fixa,
                rentabilidade_variavel = dbo.rentabilidade_variavel,
                data_atualizacao = dbo.data_atualizacao,
                vencimento = dbo.vencimento,
                valor_minimo = dbo.valor_minimo,
                tempo_minimo = dbo.tempo_minimo
            };

            _context.tblInvestimentos.Add(investimento);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("Falha ao registrar mudanças no banco de dados");
            }

            return CreatedAtAction(
                nameof(GetInvestimentos),
            new
            {
                id = investimento.id_investimento
            },
                investimento.toDbo(investimento));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteInvestimento(int id)
        {
            Investimento? carteira = await _context.tblInvestimentos.FindAsync(id);

            if (carteira == null)
                return NotFound();

            _context.tblInvestimentos.Remove(carteira);

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
        public async Task<IActionResult> AlterInvestimento(int id, InvestimentoDbo dbo)
        {
            Investimento? investimento = await _context.tblInvestimentos.FindAsync(id);

            if (investimento == null)
                return BadRequest();

            investimento.id_investimento = dbo.id_investimento;
            investimento.id_riscoInvestimento = dbo.id_riscoInvestimento;
            investimento.nome = dbo.nome;
            investimento.tipo_investimento = dbo.tipo_investimento;
            investimento.rentabilidade_fixa = dbo.rentabilidade_fixa;
            investimento.rentabilidade_variavel = dbo.rentabilidade_variavel;
            investimento.data_atualizacao = dbo.data_atualizacao;
            investimento.vencimento = dbo.vencimento;
            investimento.valor_minimo = dbo.valor_minimo;
            investimento.tempo_minimo = dbo.tempo_minimo;


            try
            {
                _context.Update(investimento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
