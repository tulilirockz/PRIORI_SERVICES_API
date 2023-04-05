namespace PRIORI_SERVICES_API.Models.Dbos
{
    public class AtualizacaoDbo
    {
        public int id_atualizacao { get; set; }
        public int id_consultor { get; set; }
        public DateTime? data_atualizacao { get; set; }
        public Decimal rentFixaAntiga { get; set; }
        public Decimal rentVarAntiga { get; set; }
        public Decimal rentFixaAtual { get; set; }
        public Decimal rentVarAtual { get; set; }
    }
}
