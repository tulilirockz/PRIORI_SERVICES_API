using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Model;
using PRIORI_SERVICES_API.Models;
using PRIORI_SERVICES_API.Models.Dbos;
using PRIORI_SERVICES_API.Repository.Interface;
using System.Data;

namespace PRIORI_SERVICES_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtualizacaoController : ControllerBase
    {
        private readonly IAtualizacaoRepository _repository;
        public AtualizacaoController(IAtualizacaoRepository repository )
        {
            _repository = repository;
        }

        [HttpGet(Name = "GetAtualizacao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Atualizacao>>> GetAtualizacao()
        {
            return await _repository.FindAll().ToListAsync();
        }

        [HttpGet("{id}", Name = "GetAtualizacaoById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Atualizacao>>> GetAtualizacaoFromID(int id)
        {
            Atualizacao? atualizacao = await _repository.GetById(id);

            if (atualizacao == null)
                return NotFound();

            return Ok(atualizacao);
        }


        [HttpPost(Name = "CreateAtualizacao")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Atualizacao>> CreateAtualizacao(AtualizacaoDbo dbo)
        {
            var novaAtualizacao = new Atualizacao
            {
                id_atualizacao = dbo.id_atualizacao,
                id_consultor = dbo.id_consultor,
                id_investimento = dbo.id_investimento,
                data_atualizacao = dbo.data_atualizacao,
                rentVarAtual = dbo.rentVarAtual,
                rentVarAntiga = dbo.rentVarAntiga,
                rentFixaAntiga = dbo.rentFixaAntiga,
                rentFixaAtual = dbo.rentFixaAtual
            };
          

            try
            {
               await _repository.Create(novaAtualizacao);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("Falha ao registrar mudanças no banco de dados");
            }

            return CreatedAtAction(
                nameof(GetAtualizacao),
            new
            {
                    id_atualizacao = novaAtualizacao.id_atualizacao
                },
                novaAtualizacao.toDbo(novaAtualizacao));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletarAtualizacao(int id)
        {
            Atualizacao? atualizacao = await _repository.GetById(id);

            if (atualizacao == null)
                return NotFound();

            try
            {
                await _repository.Delete(atualizacao.id_atualizacao);
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
        public async Task<IActionResult> AlterAtualizacao(int id, AtualizacaoDbo request)
        {
            Atualizacao? atualizacao = await _repository.GetById(id);

            if (atualizacao == null)
                return BadRequest();
    
            atualizacao.id_atualizacao = atualizacao.id_atualizacao;

            try
            {
                await _repository.Update(atualizacao);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
