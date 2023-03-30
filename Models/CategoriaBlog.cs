namespace PRIORI_SERVICES_API.Model;

public class CategoriaBlog
{
    public CategoriaBlogDBO toDBO(CategoriaBlog hi)
    {
        return new CategoriaBlogDBO
        {
            nome_categoria = hi.nome_categoria
        };
    }
    public int id_categoria { get; set; }
    public string? nome_categoria { get; set; } = null;
}