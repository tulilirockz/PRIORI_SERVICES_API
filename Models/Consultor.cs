namespace PRIORI_SERVICES_API.Model
{
    public class Consultor : Consumidor
    {
        public DateOnly? data_demissao { get; set; } = null;
        public string? usuario { get; set; } = null;
    }
}