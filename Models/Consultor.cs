using PRIORI_SERVICES_API.Model.DBO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRIORI_SERVICES_API.Model;

[PrimaryKey(nameof(id_consultor))]
[Table("tblConsultores")]
public class Consultor : Consumidor<ConsultorDBO, Consultor>
{
    [Column(TypeName = "int"), Required]
    public int id_consultor { get; set; }

    [Column(TypeName = "date")]
    public DateTime data_contratacao { get; set; }
    [Column(TypeName = "date")]
    public DateTime? data_demissao { get; set; }
    [Column(TypeName = "varchar(50)"), Required]
    public string usuario { get; set; } = String.Empty;
    public override ConsultorDBO toDBO(ref Consultor dbo)
    {
        return new ConsultorDBO
        {
            usuario = dbo.usuario,
            cpf = dbo.cpf,
            email = dbo.email,
            telefone = dbo.telefone
        };
    }
}