using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Model;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using PRIORI_SERVICES_API.Util;

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

    [HttpGet("Cliente", Name = "Login Cliente")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Cliente>> Login(string email, string senha)
    {
        List<Cliente> CheckUserExists = await (from user in _context.tblClientes
                                               where user.email == email
                                               select user).ToListAsync();
        var DEFAULT_BAD_REQUEST = "Falha ao fazer login";

        if (CheckUserExists.Count <= 0 ||
            CheckUserExists[0] == null ||
            CheckUserExists[0].email == null)
        {
            return BadRequest(DEFAULT_BAD_REQUEST);
        }

        if (!BCrypt.Net.BCrypt.Verify(senha, CheckUserExists[0].senhaHash))
        {
            return BadRequest(DEFAULT_BAD_REQUEST);
        }

        List<Claim> claims = new List<Claim> {
            new Claim(ClaimTypes.Name, CheckUserExists[0].email!),
            new Claim(ClaimTypes.Role, "User"),
        };

        return Ok(new JwtSecurityTokenHandler().WriteToken(
            JwtHandler.GenJWT(_configuration, claims)
        ));
    }

    [HttpPost("Cliente", Name = "RegistrarCliente")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Cliente>> Registrar(ClienteDbo request)
    {

        List<Cliente> CheckUserExists = await (from user in _context.tblClientes
                                               where user.email == request.email
                                               select user).ToListAsync();

        var DEFAULT_BAD_REQUEST = "Falha ao registrar usuário";

        if (CheckUserExists.Count > 0 ||
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
}
