using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRIORI_SERVICES_API.Models.Dbos;
public class ClienteDBO : ConsumidorDBO
{
    public int? id_tipoinvestidor { get; set; }
    public int? id_consultor { get; set; }
    public string? endereco { get; set; } = null;
}