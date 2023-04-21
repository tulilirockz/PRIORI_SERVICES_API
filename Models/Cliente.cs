using PRIORI_SERVICES_API.Models.Dbos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRIORI_SERVICES_API.Model;

[PrimaryKey(nameof(id_cliente))]
[Table("tblClientes")]
public class Cliente : Consumidor<ClienteDBO, Cliente>
{
    [Column(TypeName = "int")]
    public int id_cliente { get; set; }
    [Column(TypeName = "int")]
    public int? id_tipoinvestidor { get; set; }
    [Column(TypeName = "int"), ForeignKey("Consultor")]
    public int? id_consultor { get; set; }
    [Column(TypeName = "varchar")]
    public string? endereco { get; set; }
    [Column(TypeName = "varchar(40)")]
    public string? nome { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? data_adesao { get; set; }
    [Column(TypeName = "numeric")]
    public float? pontuacao { get; set; }

    public override ClienteDBO toDBO(ref Cliente DBO)
    {
        return new ClienteDBO
        {
            cpf = DBO.cpf,
            email = DBO.email,
            endereco = DBO.endereco,
            id_consultor = DBO.id_consultor,
            id_tipoinvestidor = DBO.id_tipoinvestidor,
            nome = DBO.nome,
            telefone = DBO.telefone
        };
    }
}