namespace PRIORI_SERVICES_API.Model;
public class ClienteDbo : ConsumidorDbo
{
    public int? id_tipoinvestidor { get; set; }
    public int? id_consultor { get; set; }
    public string? endereco { get; set; } = null;
}