using PRIORI_SERVICES_API.Models.Dbos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRIORI_SERVICES_API.Model;

[Table("tblPostBlog")]
[PrimaryKey(nameof(id_post))]
public class PostBlog
{
    public PostBlogDBO toDBO(PostBlog DBO)
    {
        return new PostBlogDBO
        {
            id_autor = DBO.id_autor,
            id_categoria = DBO.id_categoria,
            titulo = DBO.titulo,
            conteudo = DBO.conteudo,
            descricao = DBO.descricao
        };
    }
    [Column(TypeName = "int")]
    public int id_post { get; set; }
    [Column(TypeName = "int")]
    public int id_autor { get; set; }
    [Column(TypeName = "int")]
    public int id_categoria { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime data_criacao { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string? titulo { get; set; } = null;
    [Column(TypeName = "varchar(200)")]
    public string? descricao { get; set; } = null;
    [Column(TypeName = "varchar(2000)")]
    public string? conteudo { get; set; } = null;
}