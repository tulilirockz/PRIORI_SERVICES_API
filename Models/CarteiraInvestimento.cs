using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRIORI_SERVICES_API.Model;

[Table("tblCarteiraInvestimentos")]
[PrimaryKey(nameof(id_efetuacao))]
public class CarteiraInvestimento
{
    [Column(TypeName = "int")]
    public int id_efetuacao { get; set; }

    [Column(TypeName = "int"), ForeignKey("tblClientes")]
    public int id_cliente_carteira { get; set; }

    [Column(TypeName = "int"), ForeignKey("tblInvestimentos")]
    public int id_investimento { get; set; }

    [Column(TypeName = "numeric(8,4)"), ForeignKey("tblInvestimentos")]
    public Decimal rentabilidade_fixa { get; set; }
    [Column(TypeName = "numeric(8,2)")]
    public Decimal rentabilidade_variavel { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime data_efetuacao { get; set; }
    [Column(TypeName = "numeric(8,2)")]
    public Decimal valor_aplicado { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? data_encerramento { get; set; }
    [Column(TypeName = "varchar(8)")]
    public string? status { get; set; }
    [Column(TypeName = "numeric(8,2)")]
    public Decimal saldo { get; set; }
    public CarteiraInvestimentoDBO toDBO()
    {
        return new CarteiraInvestimentoDBO
        {
            id_efetuacao = this.id_efetuacao,
            id_cliente_carteira = this.id_cliente_carteira,
            id_investimento = this.id_investimento,
            rentabilidade_fixa = this.rentabilidade_fixa,
            rentabilidade_variavel = this.rentabilidade_variavel,
            valor_aplicado = this.valor_aplicado,
            data_encerramento = this.data_encerramento,
            data_efetuacao = this.data_efetuacao,
            status = this.status,
            saldo = this.saldo
        };
    }
}