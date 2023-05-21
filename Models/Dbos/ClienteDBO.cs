namespace PRIORI_SERVICES_API.Model.DBO;
public sealed class ClienteDBO
{
    public int? id_tipoinvestidor { get; set; }
    public int? id_consultor { get; set; }
    public string? nome { get; set; }
    public string? cpf { get; set; }
    public string? status { get; set; }
    public DateTime? data_adesao { get; set; }
    public RespostaAssessoria? respostaAssessoria { get; set; }
    public SituacaoAssessoria? situacaoAssessoria { get; set; }
    public string? endereco { get; set; }
    public DateTime? dataNascimento { get; set; }
    public string? email { get; set; }
    public string? senha { get; set; }
}