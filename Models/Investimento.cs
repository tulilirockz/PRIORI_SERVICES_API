using PRIORI_SERVICES_API.Model.DBO;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRIORI_SERVICES_API.Model;

[PrimaryKey(nameof(id_investimento))]
[Table("tblInvestimentos")]
public class Investimento
{

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
    public DateOnly vencimento { get; set; }
    [Column(TypeName = "numeric(8,2)")]
    public Decimal valor_minimo { get; set; }
    [Column(TypeName = "numeric(3)")]
    public Decimal tempo_minimo { get; set; }
    public InvestimentoDBO toDBO()
    {
        return new InvestimentoDBO
        {
            id_investimento = this.id_investimento,
            id_riscoInvestimento = this.id_riscoInvestimento,
            nome = this.nome,
            tipo_investimento = this.tipo_investimento,
            rentabilidade_fixa = this.rentabilidade_fixa,
            rentabilidade_variavel = this.rentabilidade_variavel,
            data_atualizacao = this.data_atualizacao,
            vencimento = this.vencimento,
            valor_minimo = this.valor_minimo,
            tempo_minimo = this.tempo_minimo
        };
    }
}

