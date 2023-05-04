using PRIORI_SERVICES_API.Model.DBO;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRIORI_SERVICES_API.Model;

[Table("tblCategoriaBlog")]
[PrimaryKey(nameof(id_categoria))]
public class CategoriaBlog
{
    public CategoriaBlogDBO toDBO(CategoriaBlog dbo)
    {
        return new CategoriaBlogDBO
        {
            nome_categoria = dbo.nome_categoria
        };
    }
    [Column(TypeName = "int")]
    public int id_categoria { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string? nome_categoria { get; set; } = null;
}