using PRIORI_SERVICES_API.Model;
using System.Text.Json.Serialization;

namespace PRIORI_SERVICES_API.Models;
public class CarteiraInvestimentoDBO
{
    public int id_efetuacao { get; set; }

    [JsonIgnore]
    public Cliente? cliente { get; set; }
    public int id_cliente_carteira { get; set; }

    [JsonIgnore]
    public Investimento? investimento { get; set; }
    public int id_investimento { get; set; }

    public Decimal rentabilidade_fixa { get; set; }
    public Decimal rentabilidade_variavel { get; set; }
    public DateTime data_efetuacao { get; set; }
    public Decimal valor_aplicado { get; set; }
    public DateTime? data_encerramento { get; set; }
    public string? status { get; set; }
    public Decimal saldo { get; set; }
}
