using PRIORI_SERVICES_API.Models.Dbos;

namespace PRIORI_SERVICES_API.Model;

public class CategoriaBlog
{
    public CategoriaBlogDBO toDBO(CategoriaBlog dbo)
    {
        return new CategoriaBlogDBO
        {
            nome_categoria = dbo.nome_categoria
        };
    }
    public int id_categoria { get; set; }
    public string? nome_categoria { get; set; } = null;
}