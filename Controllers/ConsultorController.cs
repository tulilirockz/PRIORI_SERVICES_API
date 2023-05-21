using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Model;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using PRIORI_SERVICES_API.Repository;
using Microsoft.AspNetCore.Authorization;
using PRIORI_SERVICES_API.Model.DBO;
using PRIORI_SERVICES_API.Model.Request;

namespace PRIORI_SERVICES_API.Controllers;
[Route("api/Auth/[controller]")]
[ApiController]
public class ConsultorController : ControllerBase
{
    private readonly PrioriDbContext _context;
    private readonly IConfiguration _configuration;


    public ConsultorController(PrioriDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("login", Name = "Login Consultor")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Consultor>> Login(ConsultorRequestDBO request)
    {

        Consultor? selected_consultor;

        try
        {
            selected_consultor = await (from consultor in _context.tblConsultores
                                        where consultor.usuario == request.usuario
                                        select consultor).SingleAsync();
        }
        catch (Exception)
        {
            selected_consultor = null;
        }

        if (selected_consultor == null ||
            selected_consultor.usuario == null ||
            selected_consultor.status == "INATIVO" ||
            !BCrypt.Net.BCrypt.Verify(request.senha, selected_consultor.senhaHash))
        {
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);
        }

        var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, selected_consultor.email!),
            new Claim(ClaimTypes.Role, "Consultor"),
            new Claim(ClaimTypes.Sid, selected_consultor.usuario.ToString())
        };

        return Ok(new JwtSecurityTokenHandler().WriteToken(
                JwtHandler.GenerateJWTToken(_configuration, claims)
        ));
    }

    [HttpPost("register", Name = "RegistrarConsultor"), Authorize(Roles = "Consultor")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Consultor>> Registrar(ConsultorDBO request)
    {

        bool CheckUserExists = _context.tblConsultores.Any(e => e.usuario == request.usuario);

        if (CheckUserExists ||
            request.usuario == null ||
            !request.senha!.Any(char.IsUpper) ||
            !request.senha!.Any(char.IsSymbol) ||
            !request.senha!.Any(char.IsNumber) ||
            request.senha!.Length <= 8)
        {
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);
        }

        string senhaSalt = BCrypt.Net.BCrypt.GenerateSalt();

        var novoConsultor = new Consultor
        {
            data_contratacao = System.TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")),
            senhaHash = BCrypt.Net.BCrypt.HashPassword(request.senha, senhaSalt),
            senhaSalt = senhaSalt,
            cpf = request.cpf,
            email = request.email,
            usuario = request.usuario,
            telefone = request.telefone,
            status = "ATIVO"
        };

        _context.tblConsultores.Add(novoConsultor);
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
                id = novoConsultor.id_consultor,
                data_criacao = novoConsultor.data_contratacao,
            },
            novoConsultor.toDBO());
    }

    [HttpDelete("{id}"), Authorize(Roles = "Consultor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        Consultor? selected_consultor = await _context.tblConsultores.FindAsync(id);

        if (selected_consultor == null)
            return NotFound();

        selected_consultor.status = "INATIVO";

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e) when (e is DbUpdateConcurrencyException || e is DbUpdateException)
        {
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);
        }

        return Ok(selected_consultor);
    }


    [HttpPut("{id}", Name = "AlterConsultorDetails"), Authorize(Roles = "Consultor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Alter(int id, ConsultorDBO request)
    {
        Consultor? selected_consultor = await _context.tblConsultores.FindAsync(id);

        if (selected_consultor == null)
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);

        selected_consultor.cpf = request.cpf;
        selected_consultor.email = request.email;
        selected_consultor.telefone = request.telefone;

        if (request.senha != null)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            selected_consultor.senhaHash = BCrypt.Net.BCrypt.HashPassword(request.senha, salt);
            selected_consultor.senhaSalt = salt;
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e) when (e is DbUpdateConcurrencyException || e is DbUpdateException)
        {
            return BadRequest(DefaultRequest.DEFAULT_BAD_REQUEST);
        }

        return Ok(selected_consultor);
    }
}
