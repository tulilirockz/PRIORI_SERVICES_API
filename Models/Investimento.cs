namespace PRIORI_SERVICES_API.Models
{
    public class Investimento
    {
        public int id_investtimento { get; set; }
        public Decimal id_riscoInvestimento { get; set; }
        public string nome { get; set; }
        public string tipo_investimento { get; set; }
        public Decimal rentabilidade_fixa { get; set; }
        public Decimal rentabilidade_variavel { get; set; }
        public DateTime data_atualizacao { get; set; }
        public DateTime vencimento { get; set; }
        public Decimal valor_minimo { get; set; }
        public Decimal tempo_minimo { get; set; }
    }
}
