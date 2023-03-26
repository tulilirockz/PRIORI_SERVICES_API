namespace PRIORI_SERVICES_API.Model
{
    public class Usuario : Consumidor
    {
        public int id_tipoinvestidor { get; set; }
        public int id_consultor { get; set; }
        public float? pontuacao { get; set; } = null;
        public string? endereco { get; set; } = null;
    }
}