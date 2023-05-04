using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRIORI_SERVICES_API.Model;

[Table("tblCarteiraInvestimentos")]
[PrimaryKey(nameof(id_investimento))]
public class CarteiraInvestimento
{
    public CarteiraInvestimentoDBO toDbo(CarteiraInvestimento ct)
    {
        return new CarteiraInvestimentoDBO
        {
            id_efetuacao = ct.id_efetuacao,
            id_cliente_carteira = ct.id_cliente_carteira,
            id_investimento = ct.id_investimento,
            rentabilidade_fixa = ct.rentabilidade_fixa,
            rentabilidade_variavel = ct.rentabilidade_variavel,
            valor_aplicado = ct.valor_aplicado,
            data_encerramento = ct.data_encerramento,
            data_efetuacao = ct.data_efetuacao,
            status = ct.status,
            saldo = ct.saldo
        };
    }
    [Column(TypeName = "int")]
    public int id_efetuacao { get; set; }

    [Column(TypeName = "int")]
    [ForeignKey("tblClientes")]
    public int id_cliente_carteira { get; set; }

    [Column(TypeName = "int")]
    [ForeignKey("tblInvestimentos")]
    public int id_investimento { get; set; }

    [Column(TypeName = "numeric(8,4)")]
    [ForeignKey("tblInvestimentos")]
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
}