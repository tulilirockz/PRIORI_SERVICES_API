using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Model;
using PRIORI_SERVICES_API.Model.DBO;
using PRIORI_SERVICES_API.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using PRIORI_SERVICES_API.Shared;

namespace PRIORI_SERVICES_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AtualizacaoController : ControllerBase
{
    private readonly IAtualizacaoRepository _repository;

    private readonly PrioriDbContext _context;

    public AtualizacaoController(IAtualizacaoRepository repository, PrioriDbContext context)
    {
        _repository = repository;
        _context = context;
    }


    [HttpGet(Name = "GetAllAtualizacao")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Atualizacao>>> GetAll()
    {
        return await _repository.FindAll().ToListAsync();
    }

    [HttpGet("{id}", Name = "GetAtualizacaoById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Atualizacao>>> GetFromID(int id)
    {
        Atualizacao? selected_atualizacao = await _repository.GetById(id);

        if (selected_atualizacao == null)
            return BadRequest(DefaultRequests.BAD_REQUEST);

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
            return BadRequest(DefaultRequests.BAD_REQUEST);
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

    [HttpGet("investimento/{id}", Name = "GetAtualizacaoByInvestimentoId")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Atualizacao>>> GetFromInvestimentoID(int id)
    {
        Atualizacao[]? atualizacoes_investimento;

        try
        {
            atualizacoes_investimento = await (from atualizacao in _context.tblAtualizacao where atualizacao.id_investimento == id select atualizacao).ToArrayAsync();
        }
        catch (Exception e) when (e is ArgumentException || e is OperationCanceledException)
        {
            return BadRequest(DefaultRequests.BAD_REQUEST);
        }

        return Ok(atualizacoes_investimento);
    }
}
