using PRIORI_SERVICES_API.Models.Dbos;

namespace PRIORI_SERVICES_API.Model;

public class Consultor : Consumidor<ConsultorDbo, Consultor>
{
    public int id_consultor { get; set; }
    public DateTime data_contratacao { get; set; }
    public DateTime? data_demissao { get; set; }
    public string? usuario { get; set; }
    public override ConsultorDbo toDBO(ref Consultor dbo)
    {
        return new ConsultorDbo
        {
            usuario = dbo.usuario,
            cpf = dbo.cpf,
            email = dbo.email,
            nome = dbo.nome,
            telefone = dbo.telefone
        };
    }
}