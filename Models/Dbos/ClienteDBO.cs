namespace PRIORI_SERVICES_API.Model.DBO;
public class ClienteDBO : ConsumidorDBO
{
    public int? id_tipoinvestidor { get; set; }
    public int? id_consultor { get; set; }
    public string? endereco { get; set; }
    public string? senha { get; set; }
}