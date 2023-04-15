using PRIORI_SERVICES_API.Models.Dbos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRIORI_SERVICES_API.Models;

[PrimaryKey(nameof(id_investimento))]
[Table("tblInvestimentos")]
public class Investimento
{
    public InvestimentoDbo toDbo(Investimento inv)
    {
        return new InvestimentoDbo
        {
            id_investimento = inv.id_investimento,
            id_riscoInvestimento = inv.id_riscoInvestimento,
            nome = inv.nome,
            tipo_investimento = inv.tipo_investimento,
            rentabilidade_fixa = inv.rentabilidade_fixa,
            rentabilidade_variavel = inv.rentabilidade_variavel,
            data_atualizacao = inv.data_atualizacao,
            vencimento = inv.vencimento,
            valor_minimo = inv.valor_minimo,
            tempo_minimo = inv.tempo_minimo,
        };
    }
    [Column(TypeName = "int")]
    public int id_investimento { get; set; }

    [Column(TypeName = "numeric")]
    public Decimal id_riscoInvestimento { get; set; }

    [Column(TypeName = "varchar(40)")]
    public string? nome { get; set; }

    [Column(TypeName = "varchar(5)")]
    public string? tipo_investimento { get; set; }

    [Column(TypeName = "numeric(8,4)")]
    public Decimal rentabilidade_fixa { get; set; }
    [Column(TypeName = "numeric(8,2)")]
    public Decimal rentabilidade_variavel { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime data_atualizacao { get; set; }
    [Column(TypeName = "date")]
    public DateTime vencimento { get; set; }
    [Column(TypeName = "numeric(8,2)")]
    public Decimal valor_minimo { get; set; }
    [Column(TypeName = "numeric(3)")]
    public Decimal tempo_minimo { get; set; }
}

