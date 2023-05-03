using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Model;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using PRIORI_SERVICES_API.Repository;
using Microsoft.AspNetCore.Authorization;
using PRIORI_SERVICES_API.Models.Dbos;
using PRIORI_SERVICES_API.Models;

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

    [HttpPost("login", Name = "LoginCliente")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Cliente>> Login(ClienteLoginDBO request)
    {
        Cliente? target_cliente;

        try
        {
            target_cliente = await (from user in _context.tblClientes
                                    where user.email == request.email
                                    select user).SingleAsync();
        }
        catch (Exception)
        {
            target_cliente = null;
        }

        if (target_cliente == null ||
            target_cliente.email == null ||
            target_cliente.status == "INATIVO" ||
            !BCrypt.Net.BCrypt.Verify(request.senha, target_cliente.senhaHash))
        {
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);
        }

        var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, target_cliente.email!),
            new Claim(ClaimTypes.Role, "Cliente"),
            new Claim(ClaimTypes.Sid, target_cliente.id_cliente.ToString())
        };

        return Ok(new JwtSecurityTokenHandler().WriteToken(
            JwtHandler.GenerateJWTToken(_configuration, claims)
        ));
    }

    [HttpPost("registrar", Name = "RegistrarCliente")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Cliente>> Registrar(ClienteDBO request, string senha)
    {
        var user_exists = _context.tblClientes.Any(e => e.email == request.email);

        if (user_exists ||
            request.email == null ||
            !senha!.Any(char.IsUpper) ||
            !senha!.Any(char.IsNumber) ||
            senha!.Length <= 8)
        {
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);
        }

        string senhaSalt = BCrypt.Net.BCrypt.GenerateSalt();

        var novoCliente = new Cliente
        {
            data_adesao = System.TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")),
            id_consultor = request.id_consultor,
            id_tipoinvestidor = request.id_tipoinvestidor,
            endereco = request.endereco,
            senhaHash = BCrypt.Net.BCrypt.HashPassword(senha, senhaSalt),
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
        catch (Exception e) when (e is DbUpdateConcurrencyException || e is DbUpdateException)
        {
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);
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

    [HttpPut("{id}", Name = "AlterarCliente"), Authorize(Roles = "Consultor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Alterar(int id, ClienteDBO request, string? senha, int? pontuacao)
    {
        Cliente? selected_cliente = await _context.tblClientes.FindAsync(id);

        if (selected_cliente == null)
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);

        selected_cliente.cpf = request.cpf;
        selected_cliente.email = request.email;
        selected_cliente.endereco = request.endereco;
        selected_cliente.id_consultor = request.id_consultor;
        selected_cliente.id_tipoinvestidor = request.id_tipoinvestidor;
        selected_cliente.nome = request.nome;
        selected_cliente.telefone = request.telefone;

        if (senha != null)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            selected_cliente.senhaHash = BCrypt.Net.BCrypt.HashPassword(senha, salt);
            selected_cliente.senhaSalt = salt;
        }

        if (pontuacao != null)
        {
            selected_cliente.pontuacao = pontuacao;
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e) when (e is DbUpdateConcurrencyException || e is DbUpdateException)
        {
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);
        }

        return Ok(selected_cliente);
    }

    [HttpDelete("{id}", Name = "DesativarCliente"), Authorize(Roles = "Consultor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        Cliente? selected_cliente = await _context.tblClientes.FindAsync(id);

        if (selected_cliente == null)
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);

        selected_cliente.status = "INATIVO";

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e) when (e is DbUpdateConcurrencyException || e is DbUpdateException)
        {
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);
        }

        return Ok(selected_cliente);
    }
}
