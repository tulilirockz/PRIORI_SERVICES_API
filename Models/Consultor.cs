using PRIORI_SERVICES_API.Models.Dbos;

namespace PRIORI_SERVICES_API.Model;

public class Consultor : Consumidor<ConsultorDbo, Consultor>
{
    public int id_consultor { get; set; }
    public DateTime data_contratacao { get; set; }
    public DateTime? data_demissao { get; set; }
    public string? usuario { get; set; }
    public string? senha { get; set; }
    public override ConsultorDbo toDBO(ref Consultor DBO)
    {
        return new ConsultorDbo
        {
            usuario = DBO.usuario,
            senha = DBO.senha,
            cpf = DBO.cpf,
            email = DBO.email,
            nome = DBO.nome,
            telefone = DBO.telefone
        };
    }
}