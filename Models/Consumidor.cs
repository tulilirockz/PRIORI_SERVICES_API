using System.ComponentModel.DataAnnotations.Schema;

namespace PRIORI_SERVICES_API.Model;
public abstract class Consumidor<T, B>
{

    [Column(TypeName = "varchar(11)")]
    public string? cpf { get; set; }
    [Column(TypeName = "varchar(25)")]
    public string? email { get; set; }
    [Column(TypeName = "char(15)")]
    public string? telefone { get; set; }
    [Column(TypeName = "varchar(8)")]
    public string? status { get; set; }
    [Column(TypeName = "varchar(200)")]
    public string? senhaSalt { get; set; }
    [Column(TypeName = "varchar(200)")]
    public string? senhaHash { get; set; }
    public abstract T toDBO(ref B DBO);
}