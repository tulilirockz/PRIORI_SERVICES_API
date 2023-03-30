namespace PRIORI_SERVICES_API.Model;
public abstract class Consumidor<T, B>
{
    public string? nome { get; set; }
    public string? cpf { get; set; }
    public string? email { get; set; }
    public string? telefone { get; set; }
    public string? status { get; set; }
    public string? senhaSalt { get; set; }
    public string? senhaHash { get; set; }
    public abstract T toDBO(ref B DBO);
}