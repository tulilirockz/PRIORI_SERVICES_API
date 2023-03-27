using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Model;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using PRIORI_SERVICES_API.Util;
using Microsoft.AspNetCore.Authorization;

namespace PRIORI_SERVICES_API.Controllers;
[Route("api/Auth/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly PrioriDbContext _context;
    private readonly IConfiguration _configuration;
    public ClienteController(PrioriDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpGet(Name = "ClientLogin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Cliente>> Login(string email, string senha)
    {
        Cliente? CheckUserExists = await (from user in _context.tblClientes
                                          where user.email == email
                                          select user).SingleAsync();
        var DEFAULT_BAD_REQUEST = "Falha ao fazer login";

        if (CheckUserExists == null ||
            CheckUserExists.email == null)
        {
            return BadRequest(DEFAULT_BAD_REQUEST);
        }

        if (!BCrypt.Net.BCrypt.Verify(senha, CheckUserExists.senhaHash))
        {
            return BadRequest(DEFAULT_BAD_REQUEST);
        }

        List<Claim> claims = new List<Claim> {
            new Claim(ClaimTypes.Name, CheckUserExists.email!),
            new Claim(ClaimTypes.Role, "User"),
        };

        return Ok(new JwtSecurityTokenHandler().WriteToken(
            JwtHandler.GenJWT(_configuration, claims)
        ));
    }

    [HttpPost(Name = "RegisterClient")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Cliente>> Registrar(ClienteDbo request)
    {

        Cliente? CheckUserExists = await (from user in _context.tblClientes
                                          where user.email == request.email
                                          select user).SingleAsync();

        var DEFAULT_BAD_REQUEST = "Falha ao registrar usuário";

        if (CheckUserExists == null ||
            request.email == null ||
            !request.senha!.Any(char.IsUpper) ||
            !request.senha!.Any(char.IsSymbol) ||
            !request.senha!.Any(char.IsNumber) ||
            request.senha!.Length <= 8)
        {
            return BadRequest(DEFAULT_BAD_REQUEST);
        }

        string senhaSalt = BCrypt.Net.BCrypt.GenerateSalt();

        string senhaHash = BCrypt.Net.BCrypt.HashPassword(request.senha, senhaSalt);

        var novoCliente = new Cliente
        {
            data_adesao = System.TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")),
            id_consultor = request.id_consultor,
            id_tipoinvestidor = request.id_tipoinvestidor,
            endereco = request.endereco,
            senhaHash = senhaHash,
            senhaSalt = senhaSalt,
            cpf = request.cpf,
            email = request.email,
            nome = request.nome,
            telefone = request.telefone,
            status = "ATIVO"
        };

        _context.tblClientes.Add(novoCliente);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return BadRequest(DEFAULT_BAD_REQUEST);
        }

        return CreatedAtAction(
            nameof(Registrar),
            new
            {
                id = novoCliente.id_consultor,
                data_criacao = novoCliente.data_adesao,
            },
            novoCliente.toDBO(ref novoCliente));
    }

    [HttpPut("{id}", Name = "AlterClientDetails"), Authorize(Roles = "Consultor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AlterClient(int id, ClienteDbo request, string? senha, int? pontuacao)
    {
        Cliente? SelectedClient = await _context.tblClientes.FindAsync(id);

        if (SelectedClient == null)
            return BadRequest();

        SelectedClient.cpf = request.cpf;
        SelectedClient.email = request.email;
        SelectedClient.endereco = request.endereco;
        SelectedClient.id_consultor = request.id_consultor;
        SelectedClient.id_tipoinvestidor = request.id_tipoinvestidor;
        SelectedClient.nome = request.nome;
        SelectedClient.telefone = request.telefone;

        if (senha != null)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            SelectedClient.senhaHash = BCrypt.Net.BCrypt.HashPassword(senha, salt);
            SelectedClient.senhaSalt = salt;
        }

        if (pontuacao != null)
        {
            SelectedClient.pontuacao = pontuacao;
        }

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

    [HttpDelete("{id}", Name = "DeactivateClient"), Authorize(Roles = "Consultor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCliente(int id)
    {
        Cliente? SelectedCliente = await _context.tblClientes.FindAsync(id);

        if (SelectedCliente == null)
            return NotFound();

        SelectedCliente.status = "INATIVO";

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
}
