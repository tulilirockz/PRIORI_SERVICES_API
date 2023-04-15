using PRIORI_SERVICES_API.Models.Dbos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRIORI_SERVICES_API.Models;

[Table("tblAtualizacao")]
[PrimaryKey(nameof(id_atualizacao))]
public class Atualizacao
{
    public AtualizacaoDbo toDbo(Atualizacao at)
    {
        return new AtualizacaoDbo
        {
            id_consultor = at.id_consultor,
            data_atualizacao = at.data_atualizacao,
            rentFixaAntiga = at.rentFixaAntiga,
            rentFixaAtual = at.rentFixaAtual,
            rentVarAntiga = at.rentVarAntiga,
            rentVarAtual = at.rentVarAtual
        };
    }
    [Column(TypeName = "int")]
    public int id_atualizacao { get; set; }
    [Column(TypeName = "int")]
    [ForeignKey("Consultor")]
    public int id_consultor { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? data_atualizacao { get; set; }
    [Column(TypeName = "int")]
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
