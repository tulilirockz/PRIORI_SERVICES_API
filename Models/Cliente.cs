using PRIORI_SERVICES_API.Models;
using PRIORI_SERVICES_API.Models.Dbos;

namespace PRIORI_SERVICES_API.Model;

public class Cliente : Consumidor<ClienteDbo, Cliente>
{
    public int id_cliente { get; set; }
    public int? id_tipoinvestidor { get; set; }
    public int? id_consultor { get; set; }
    public string? endereco { get; set; }
    public DateTime? data_adesao { get; set; }
    public float? pontuacao { get; set; }
    public string senha { get; set; } = String.Empty;

    public override ClienteDbo toDBO(ref Cliente DBO)
    {
        return new ClienteDbo
        {
            cpf = DBO.cpf,
            email = DBO.email,
            endereco = DBO.endereco,
            id_consultor = DBO.id_consultor,
            id_tipoinvestidor = DBO.id_tipoinvestidor,
            nome = DBO.nome,
            senha = DBO.senha,
            telefone = DBO.telefone
        };
    }
}