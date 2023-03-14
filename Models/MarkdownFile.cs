using System.Text.Json;

namespace PRIORI_SERVICES_API.Model 
{
    public class MarkdownMetadata 
    {
        public String Serialize() {
            return JsonSerializer.Serialize(this);
        }

        public int id { get ; set ; }
        public string? Autor { get ; set ; }
        public DateTime DataAlteracao { get ; set ; }
        public string? Conteudo { get ; set ; }
    }
}