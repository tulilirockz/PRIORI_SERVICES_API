namespace PRIORI_SERVICES_API.Model;
public class PostBlogDbo
{
    public int id_autor { get; set; }
    public int id_categoria { get; set; }
    public string? titulo { get; set; } = null;
    public string? descricao { get; set; } = null;
    public string? conteudo { get; set; } = null;
}