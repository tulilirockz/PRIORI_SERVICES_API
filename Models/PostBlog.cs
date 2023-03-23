namespace PRIORI_SERVICES_API.Model
{
    public class PostBlog
    {
        public PostBlogDBO toDBO(PostBlog hi)
        {
            return new PostBlogDBO
            {
                id_autor = hi.id_autor,
                id_categoria = hi.id_categoria,
                titulo = hi.titulo,
                conteudo = hi.conteudo
            };
        }
        public int id_post { get; set; }
        public int id_autor { get; set; }
        public int id_categoria { get; set; }
        public DateTime data_criacao { get; set; }
        public string? titulo { get; set; } = null;
        public string? conteudo { get; set; } = null;
    }
}