namespace PRIORI_SERVICES_API.Model
{
    public class Consumidor
    {
        public int id { get; set; }
        public string? nome { get; set; } = null;
        public string? cpf { get; set; } = null;
        public string? email { get; set; } = null;
        public string? senha { get; set; } = null;
        public string? telefone { get; set; } = null;
        public string? status { get; set; } = null;
        public DateOnly? data_criacao { get; set; } = null;
    }
}
