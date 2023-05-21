using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Model;
using PRIORI_SERVICES_API.Model.DBO;
using PRIORI_SERVICES_API.Repository.Interface;
using Microsoft.AspNetCore.Authorization;

namespace PRIORI_SERVICES_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AtualizacaoController : ControllerBase
{
    private readonly IAtualizacaoRepository _repository;

    public AtualizacaoController(IAtualizacaoRepository repository)
    {
        _repository = repository;
    }


    [HttpGet(Name = "GetAllAtualizacao"), Authorize(Roles = "Consultor,Cliente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Atualizacao>>> GetAll()
    {
        return await _repository.FindAll().ToListAsync();
    }

    [HttpGet("{id}", Name = "GetAtualizacaoById"), Authorize(Roles = "Consultor,Cliente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Atualizacao>>> GetFromID(int id)
    {
        Atualizacao? selected_atualizacao = await _repository.GetById(id);

        if (selected_atualizacao == null)
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);

        return Ok(selected_atualizacao);
    }


    [HttpPost(Name = "CreateAtualizacao"), Authorize(Roles = "Cliente")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Atualizacao>> Create(AtualizacaoDBO dbo)
    {
        var novaAtualizacao = new Atualizacao
        {
            id_investimento = dbo.id_investimento,
            id_consultor = dbo.id_consultor,
            rentVarAtual = dbo.rentVarAtual,
            rentVarAntiga = dbo.rentVarAntiga,
            rentFixaAntiga = dbo.rentFixaAntiga,
            rentFixaAtual = dbo.rentFixaAtual
        };

        try
        {
            await _repository.Create(novaAtualizacao);
        }
        catch (Exception)
        {
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);
        }

        return CreatedAtAction(
            nameof(GetAll),
            new
            {
                id_atualizacao = novaAtualizacao.id_atualizacao,
                data_atualizacao = novaAtualizacao.data_atualizacao
            },
            novaAtualizacao.toDBO());
    }

    [HttpDelete("{id}"), Authorize(Roles = "Consultor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        Atualizacao? atualizacao = await _repository.GetById(id);

        if (atualizacao == null)
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);

        try
        {
            await _repository.Delete(atualizacao.id_atualizacao);
        }
        catch (Exception e) when (e is DbUpdateConcurrencyException || e is DbUpdateException)
        {
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);
        }

        return Ok(atualizacao);
    }

    [HttpPut("{id}"), Authorize(Roles = "Consultor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Alter(int id, AtualizacaoDBO request)
    {
        Atualizacao? atualizacao;

        try
        {
            atualizacao = await _repository.GetById(id);
        }
        catch (InvalidOperationException)
        {
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);
        }

        if (atualizacao == null)
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);

        atualizacao.data_atualizacao = request.data_atualizacao;
        atualizacao.rentFixaAntiga = request.rentFixaAntiga;
        atualizacao.rentFixaAtual = request.rentFixaAtual;
        atualizacao.rentFixaAtual = request.rentVarAntiga;
        atualizacao.rentVarAtual = request.rentVarAtual;

        try
        {
            await _repository.Update(atualizacao);
        }
        catch (Exception e) when (e is DbUpdateConcurrencyException || e is DbUpdateException)
        {
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);
        }

        return Ok(atualizacao);
    }
}
