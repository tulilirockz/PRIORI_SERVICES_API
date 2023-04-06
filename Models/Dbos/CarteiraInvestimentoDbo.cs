namespace PRIORI_SERVICES_API.Models.Dbos;
public class CarteiraInvestimentoDbo
{
    public int id_efetuacao { get; set; }
    public int id_cliente_carteira { get; set; }
    public int id_investimento { get; set; }
    public Decimal rentabilidade_fixa { get; set; }
    public Decimal rentabilidade_variavel { get; set; }
    public DateTime data_efetuacao { get; set; }
    public Decimal valor_aplicado { get; set; }
    public DateTime? data_encerramento { get; set; }
    public string status { get; set; }
    public Decimal saldo { get; set; }
}
