using System.Data.SqlTypes;

namespace PRIORI_SERVICES_API.Model.DBO;
public sealed class ClienteDBO
{
    public int? id_tipoinvestidor { get; set; }
    public int? id_consultor { get; set; }
    public string? nome { get; set; }
    public string? cpf { get; set; }
    public string? status { get; set; } = "ATIVO";
    public RespostaAssessoria? respostaAssessoria { get; set; }
    public SituacaoAssessoria? situacaoAssessoria { get; set; }
    public string? endereco { get; set; }
    public string dataNascimento { get; set; } = String.Empty;
    public string? email { get; set; }
    public string? senha { get; set; }
}