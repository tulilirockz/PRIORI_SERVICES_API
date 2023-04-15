using PRIORI_SERVICES_API.Models.Dbos;

namespace PRIORI_SERVICES_API.Models
{
    public class Investimento
    {
        public InvestimentoDbo toDbo(Investimento inv)
        {
            return new InvestimentoDbo
            {
                id_investimento = inv.id_investimento,
                id_riscoInvestimento = inv.id_riscoInvestimento,
                nome = inv.nome,
                tipo_investimento = inv.tipo_investimento,
                rentabilidade_fixa = inv.rentabilidade_fixa,
                rentabilidade_variavel = inv.rentabilidade_variavel,
                data_atualizacao = inv.data_atualizacao,
                vencimento = inv.vencimento,
                valor_minimo = inv.valor_minimo,
                tempo_minimo = inv.tempo_minimo,
            };
        }
        public int id_investimento { get; set; }
        public Decimal id_riscoInvestimento { get; set; }
        public string? nome { get; set; }
        public string? tipo_investimento { get; set; }
        public Decimal rentabilidade_fixa { get; set; }
        public Decimal rentabilidade_variavel { get; set; }
        public DateTime data_atualizacao { get; set; }
        public DateTime vencimento { get; set; }
        public Decimal valor_minimo { get; set; }
        public Decimal tempo_minimo { get; set; }
    }
}
