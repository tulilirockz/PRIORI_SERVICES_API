using PRIORI_SERVICES_API.Models;

namespace PRIORI_SERVICES_API.Repository;
public class AtualizacaoRepository : RepositoryGenerico<Atualizacao>
{
    private readonly PrioriDbContext _context;
    public AtualizacaoRepository(PrioriDbContext prioriDbContext) : base(prioriDbContext)
    {
        _context = prioriDbContext;
    }
}

