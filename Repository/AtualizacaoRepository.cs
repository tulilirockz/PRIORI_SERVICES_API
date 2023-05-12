using PRIORI_SERVICES_API.Model;
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

