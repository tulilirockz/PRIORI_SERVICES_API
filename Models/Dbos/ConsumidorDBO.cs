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

    [Required]
    [DataType(DataType.Password)]
    [Column(TypeName = "varchar")]
    [StringLength(maximumLength: 255, MinimumLength = 8)]
    public string senha { get; set; } = String.Empty;
}