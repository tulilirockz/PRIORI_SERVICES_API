namespace PRIORI_SERVICES_API.Model
{
    public class PostBlogDBO 
    {
        public int id_autor { get ; set ;}
        public string? titulo { get ; set ; } = null;

        public string? categoria { get ; set ; } = null;
        public string? conteudo { get; set ;} = null;
    }
}