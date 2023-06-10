using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Model;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using PRIORI_SERVICES_API.Repository;
using Microsoft.AspNetCore.Authorization;
using PRIORI_SERVICES_API.Model.DBO;
using PRIORI_SERVICES_API.Model.Request;
using PRIORI_SERVICES_API.Shared;

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
    public async Task<ActionResult<Cliente>> Login(ClienteRequestDBO request)
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
            return BadRequest(DefaultRequests.BAD_REQUEST);
        }

        var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, target_cliente.email!),
            new Claim(ClaimTypes.Role, "Cliente"),
            new Claim(ClaimTypes.Sid, target_cliente.id_cliente.ToString())
        };

        Dictionary<string, string> response = new Dictionary<string, string> {
            { "id", $"{target_cliente.id_cliente}" },
            { "jwt_key", $"{new JwtSecurityTokenHandler().WriteToken(JwtHandler.GenerateJWTToken(_configuration, claims))}" }
        };

        return Ok(response);
    }

    [HttpGet("info/{id}", Name = "InfoCliente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Cliente>> InfoCliente(int id)
    {
        Cliente? target_cliente;

        try
        {
            target_cliente = await _context.tblClientes.FindAsync(id);
        }
        catch (Exception)
        {
            target_cliente = null;
        }

        if (target_cliente == null)
            return BadRequest(DefaultRequests.BAD_REQUEST);

        return Ok(target_cliente);
    }


    [HttpPost("registrar", Name = "RegistrarCliente")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Cliente>> Registrar(ClienteDBO request)
    {

        var user_exists = _context.tblClientes.Any(e => e.email == request.email);

        if (user_exists ||
            request.email == null ||
            !request.senha!.Any(char.IsUpper) ||
            !request.senha!.Any(char.IsNumber) ||
            request.senha!.Length <= 8)
        {
            return BadRequest(DefaultRequests.BAD_REQUEST);
        }

        string senhaSalt = BCrypt.Net.BCrypt.GenerateSalt();

        Cliente novoCliente = new Cliente
        {
            data_adesao = System.TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(EnvironmentVariables.DATABASE_LOCALE)),
            id_consultor = request.id_consultor,
            id_tipoinvestidor = request.id_tipoinvestidor,
            endereco = request.endereco,
            senhaHash = BCrypt.Net.BCrypt.HashPassword(request.senha, senhaSalt),
            senhaSalt = senhaSalt,
            cpf = request.cpf,
            email = request.email,
            nome = request.nome,
            dataNascimento = request.dataNascimento,
            pontuacao = 0,
            respostaAssessoria = RespostaAssessoria.aceitou,
            status = "ATIVO"
        };


        _context.tblClientes.Add(novoCliente);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e) when (e is DbUpdateConcurrencyException || e is DbUpdateException)
        {
            return BadRequest(DefaultRequests.BAD_REQUEST);
        }

        Cliente? cliente_criado = null;

        try
        {
            cliente_criado = await (from user in _context.tblClientes
                                    where user.email == request.email
                                    select user).SingleAsync();
        }
        catch (Exception)
        {
            cliente_criado = null;
        }

        if (cliente_criado == null)
            return BadRequest(DefaultRequests.BAD_REQUEST);

        var carteira_criada = new CarteiraInvestimento
        {
            saldo = 0,
            valor_aplicado = 0,
            status = "ATIVO",
            id_investimento = 1,
            id_cliente_carteira = cliente_criado.id_cliente,
            data_efetuacao = System.TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(EnvironmentVariables.DATABASE_LOCALE)),
            data_encerramento = System.TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(EnvironmentVariables.DATABASE_LOCALE)),
            rentabilidade_variavel = 0,
            rentabilidade_fixa = 0
        };

        _context.tblCarteiraInvestimentos.Add(carteira_criada);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e) when (e is DbUpdateConcurrencyException || e is DbUpdateException)
        {
            return BadRequest(DefaultRequests.BAD_REQUEST);
        }

        return CreatedAtAction(
            nameof(Registrar),
            new
            {
                id = novoCliente.id_cliente,
                data_adesao = novoCliente.data_adesao,
            },
            novoCliente.toDBO());
    }

    [HttpPut("{id}", Name = "AlterarCliente"), Authorize(Roles = "Cliente,Consultor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Cliente>> Alterar(int id, ClienteAlterar request)
    {
        Cliente? selected_cliente = await _context.tblClientes.FindAsync(id);

        if (selected_cliente == null)
            return BadRequest(DefaultRequests.BAD_REQUEST);

        if(request.email != null)
            selected_cliente.email = request.email;
        
        if(request.endereco != null)
            selected_cliente.endereco = request.endereco;

        if (request.pontuacao != null) 
            selected_cliente.pontuacao = request.pontuacao;

        if (request.senha != null)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            selected_cliente.senhaHash = BCrypt.Net.BCrypt.HashPassword(request.senha, salt);
            selected_cliente.senhaSalt = salt;
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e) when (e is DbUpdateConcurrencyException || e is DbUpdateException)
        {
            return BadRequest(DefaultRequests.BAD_REQUEST);
        }

        return Ok(selected_cliente);
    }

    [HttpPut("senha/reset", Name = "ResetSenha")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Cliente>> ResetSenha(PasswordReset request)
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
            target_cliente.status == "INATIVO")
        {
            return BadRequest(DefaultRequests.BAD_REQUEST);
        }

        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        target_cliente.senhaHash = BCrypt.Net.BCrypt.HashPassword(request.senha, salt);
        target_cliente.senhaSalt = salt;

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


    [HttpDelete("{id}", Name = "DesativarCliente"), Authorize(Roles = "Consultor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        Cliente? selected_cliente = await _context.tblClientes.FindAsync(id);

        if (selected_cliente == null)
            return BadRequest(DefaultRequests.BAD_REQUEST);

        selected_cliente.status = "INATIVO";

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e) when (e is DbUpdateConcurrencyException || e is DbUpdateException)
        {
            return BadRequest(DefaultRequests.BAD_REQUEST);
        }

        return Ok(selected_cliente);
    }

    [HttpGet("idconsultor/{id}", Name = "ClienteIsConsultor"), Authorize(Roles = "Cliente")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> GetClienteConsultorID(int id) {
        // TODO: WARNING: Isso pode ser uma falha de segurança, pois um cliente pode criar uma conta com o email do consultor, talvez criar um perfil de cliente p/ o consultor inicialmente seja uma boa ideia p/ evitar esse tipo de erro.
        // Ou também, é possível criar um atributo no banco de dados, mas infelizmente isso não será possível de se implementar agora.
        Cliente? selected_cliente = await _context.tblClientes.FindAsync(id);

        if (selected_cliente == null)
            return BadRequest(DefaultRequests.BAD_REQUEST);

        Consultor? selected_consultor;

        try
        {
            selected_consultor = await (from consultor in _context.tblConsultores
                                        where consultor.email == selected_cliente.email
                                        select consultor).SingleAsync();
        }
        catch (Exception)
        {
            selected_consultor = null;
        }

        if (selected_consultor == null ||
            selected_consultor.usuario == null ||
            selected_consultor.status == "INATIVO" ||
            selected_cliente.status == "INATIVO" ||
            selected_cliente.email == null)
        {
            return BadRequest(DefaultRequests.BAD_REQUEST);
        }

        return Ok(selected_consultor.id_consultor);
    }
}
