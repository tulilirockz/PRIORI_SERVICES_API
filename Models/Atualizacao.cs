using PRIORI_SERVICES_API.Model.DBO;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRIORI_SERVICES_API.Model;

[Table("tblAtualizacao")]
[PrimaryKey(nameof(id_atualizacao))]
public class Atualizacao
{
    public AtualizacaoDBO toDbo()
    {
        return new AtualizacaoDBO
        {
            id_investimento = this.id_investimento,
            id_consultor = this.id_consultor,
            data_atualizacao = this.data_atualizacao,
            rentFixaAntiga = this.rentFixaAntiga,
            rentFixaAtual = this.rentFixaAtual,
            rentVarAntiga = this.rentVarAntiga,
            rentVarAtual = this.rentVarAtual
        };
    }
    [Column(TypeName = "int")]
    public int id_atualizacao { get; set; }
    [Column(TypeName = "int")]
    [ForeignKey("tblConsultores")]
    public int id_consultor { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? data_atualizacao { get; set; }
    [Column(TypeName = "int")]
    [ForeignKey("tblInvestimentos")]
    public int id_investimento { get; set; }
    [Column(TypeName = "numeric(8,4)")]
    public Decimal rentFixaAntiga { get; set; }
    [Column(TypeName = "numeric(8,2)")]
    public Decimal rentVarAntiga { get; set; }
    [Column(TypeName = "numeric(8,4)")]
    public Decimal rentFixaAtual { get; set; }
    [Column(TypeName = "numeric(8,2)")]
    public Decimal rentVarAtual { get; set; }
}
