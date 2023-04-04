using PRIORI_SERVICES_API.Models.Dbos;

namespace PRIORI_SERVICES_API.Models
{
    public class Atualizacao
    {
        public AtualizacaoDbo toDbo(Atualizacao at)
        {
            return new AtualizacaoDbo
            {
                id_atualizacao = at.id_atualizacao,
                id_consultor = at.id_consultor,
                data_atualizacao = at.data_atualizacao,
                rentFixaAntiga = at.rentFixaAntiga,
                rentFixaAtual = at.rentFixaAtual,
                rentVarAntiga = at.rentVarAntiga,
                rentVarAtual = at.rentVarAtual
            };
        }
        public int id_atualizacao { get; set; }
        public int id_consultor { get; set; }
        public DateTime? data_atualizacao { get; set; }
        public Decimal rentFixaAntiga { get; set; }
        public Decimal rentVarAntiga { get; set; }
        public Decimal rentFixaAtual { get; set; }
        public Decimal rentVarAtual { get; set; }

    }
}
