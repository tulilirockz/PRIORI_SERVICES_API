using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRIORI_SERVICES_API.Models.Dbos;

public class ConsumidorDBO
{
    public string? nome { get; set; }
    public string? cpf { get; set; }
    public string? email { get; set; }
    public string? telefone { get; set; }
}