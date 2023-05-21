using PRIORI_SERVICES_API.Model.DBO;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRIORI_SERVICES_API.Model;

public enum RespostaAssessoria { aceitou, recusou, cancelou }
public enum SituacaoAssessoria { ativa, inativa }

[PrimaryKey(nameof(id_cliente))]
[Table("tblClientes")]
public class Cliente
{
    [Column(TypeName = "int")]
    public int id_cliente { get; set; }

    [Column(TypeName = "int")]
    public int? id_tipoinvestidor { get; set; }

    [Column(TypeName = "int"), ForeignKey("Consultor")]
    public int? id_consultor { get; set; }

    [Column(TypeName = "varchar(40)")]
    public string? nome { get; set; }

    [Column(TypeName = "varchar(11)")]
    public string? cpf { get; set; }

    [Column(TypeName = "varchar(8)")]
    public string? status { get; set; }

    [Column(TypeName = "date")]
    public DateTime data_adesao { get; set; }

    [Column(TypeName = "numeric")]
    public float? pontuacao { get; set; }

    [Column(TypeName = "int")]
    public RespostaAssessoria? respostaAssessoria { get; set; }
    [Column(TypeName = "int")]
    public SituacaoAssessoria? situacaoAsessoria { get; set; }

    [Column(TypeName = "varchar(60)")]
    public string? endereco { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? dataNascimento { get; set; }

    [Column(TypeName = "varchar(25)")]
    public string? email { get; set; }

    [Column(TypeName = "varchar(200)")]
    public string? senhaSalt { get; set; }

    [Column(TypeName = "varchar(200)")]
    public string? senhaHash { get; set; }

    public ClienteDBO toDBO()
    {
        return new ClienteDBO
        {
            cpf = this.cpf,
            email = this.email,
            endereco = this.endereco,
            id_consultor = this.id_consultor,
            id_tipoinvestidor = this.id_tipoinvestidor,
            nome = this.nome,
            dataNascimento = this.dataNascimento,

        };
    }
}