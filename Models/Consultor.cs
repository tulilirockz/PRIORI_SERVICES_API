using PRIORI_SERVICES_API.Model.DBO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRIORI_SERVICES_API.Model;

[PrimaryKey(nameof(id_consultor))]
[Table("tblConsultores")]
public class Consultor
{

    [Column(TypeName = "int"), Required]
    public int id_consultor { get; set; }

    [Column(TypeName = "varchar(40)")]
    public string nome { get; set; } = String.Empty;

    [Column(TypeName = "date")]
    public DateOnly dataNascimento { get; set; }

    [Column(TypeName = "varchar(11)")]
    public string? cpf { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string? email { get; set; }

    [Column(TypeName = "char(15)")]
    public string? telefone { get; set; }

    [Column(TypeName = "date")]
    public DateOnly data_contratacao { get; set; }

    [Column(TypeName = "date")]
    public DateOnly? data_demissao { get; set; }

    [Column(TypeName = "varchar(8)")]
    public string? status { get; set; }

    [Column(TypeName = "varchar(50)"), Required]
    public string usuario { get; set; } = String.Empty;

    [Column(TypeName = "varchar(400)")]
    public string? senhaSalt { get; set; }

    [Column(TypeName = "varchar(400)")]
    public string? senhaHash { get; set; }
    public ConsultorDBO toDBO()
    {
        return new ConsultorDBO
        {
            usuario = this.usuario,
            cpf = this.cpf,
            email = this.email,
            nome = this.nome,
            status = this.status,
            dataNascimento = this.dataNascimento,
            telefone = this.telefone
        };
    }
}