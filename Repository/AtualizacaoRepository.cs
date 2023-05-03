using PRIORI_SERVICES_API.Models;
using PRIORI_SERVICES_API.Repository.Interface;

namespace PRIORI_SERVICES_API.Repository
{
    public class AtualizacaoRepository : RepositoryGenerico<Atualizacao>, IAtualizacaoRepository
    {
        private readonly PrioriDbContext _context;
        public AtualizacaoRepository(PrioriDbContext prioriDbContext) : base(prioriDbContext)
        {
            _context = prioriDbContext;
        }
    }
}

