using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Model;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using PRIORI_SERVICES_API.Repository;
using Microsoft.AspNetCore.Authorization;
using PRIORI_SERVICES_API.Models.Dbos;

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

    [HttpGet(Name = "Login Consultor")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Consultor>> Login(string usuario, string senha)
    {
        Consultor? SelectedConsultor = await (from consultor in _context.tblConsultores
                                              where consultor.usuario == usuario
                                              select consultor).SingleAsync();

        if (SelectedConsultor == null ||
            SelectedConsultor.usuario == null ||
            SelectedConsultor.status == "INATIVO" ||
            !BCrypt.Net.BCrypt.Verify(senha, SelectedConsultor.senhaHash))
        {
            return BadRequest("Falha ao fazer login");
        }

        var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, SelectedConsultor.email!),
            new Claim(ClaimTypes.Role, "Consultor"),
        };

        return Ok(new JwtSecurityTokenHandler().WriteToken(
                JwtHandler.GenerateJWTToken(_configuration, claims)
        ));
    }

    [HttpPost(Name = "RegistrarConsultor"), Authorize(Roles = "Consultor")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Consultor>> Registrar(ConsultorDBO request, string senha)
    {

        bool CheckUserExists = _context.tblConsultores.Any(e => e.usuario == request.usuario);

        var DEFAULT_BAD_REQUEST = "Falha ao registrar usuário";

        if (CheckUserExists ||
            request.usuario == null ||
            !senha!.Any(char.IsUpper) ||
            !senha!.Any(char.IsSymbol) ||
            !senha!.Any(char.IsNumber) ||
            senha!.Length <= 8)
        {
            return BadRequest(DEFAULT_BAD_REQUEST);
        }

        string senhaSalt = BCrypt.Net.BCrypt.GenerateSalt();

        var novoConsultor = new Consultor
        {
            data_contratacao = System.TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")),
            senhaHash = BCrypt.Net.BCrypt.HashPassword(senha, senhaSalt),
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
        catch (DbUpdateConcurrencyException)
        {
            return BadRequest(DEFAULT_BAD_REQUEST);
        }

        return CreatedAtAction(
            nameof(Registrar),
            new
            {
                id = novoConsultor.id_consultor,
                data_criacao = novoConsultor.data_contratacao,
            },
            novoConsultor.toDBO(ref novoConsultor));

    }

    [HttpDelete("{id}"), Authorize(Roles = "Consultor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteConsultor(int id)
    {
        Consultor? SelectedConsultor = await _context.tblConsultores.FindAsync(id);

        if (SelectedConsultor == null)
            return NotFound();

        SelectedConsultor.status = "INATIVO";

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


    [HttpPut("{id}", Name = "AlterConsultorDetails"), Authorize(Roles = "Consultor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AlterConsultor(int id, ConsultorDBO ConsultorDbo, string? senha)
    {
        Consultor? SelectedConsultor = await _context.tblConsultores.FindAsync(id);

        if (SelectedConsultor == null)
            return BadRequest();

        SelectedConsultor.cpf = ConsultorDbo.cpf;
        SelectedConsultor.email = ConsultorDbo.email;
        SelectedConsultor.telefone = ConsultorDbo.telefone;

        if (senha != null)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            SelectedConsultor.senhaHash = BCrypt.Net.BCrypt.HashPassword(senha, salt);
            SelectedConsultor.senhaSalt = salt;
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
}
