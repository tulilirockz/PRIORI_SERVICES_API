namespace PRIORI_SERVICES_API.Model;

public struct ClienteAlterar
{
    public string? email { get; set; }

    public string? senha { get; set; }

    public string? endereco {get;set;}
    public float? pontuacao {get;set;}
}