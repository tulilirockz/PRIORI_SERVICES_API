using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Model;
id_investimento
public class PrioriDbContext : DbContext
{
    public PrioriDbContext(DbContextOptions<PrioriDbContext> options) : base(options)
    {
    }

    public DbSet<PostBlog> tblPostBlog { get; set; } = null!;
    public DbSet<CategoriaBlog> tblCategoriaBlog { get; set; } = null!;
    public DbSet<Consultor> tblConsultores { get; set; } = null!;
    public DbSet<Cliente> tblClientes { get; set; } = null!;
    public DbSet<Atualizacao> tblAtualizacao { get; set; } = null!;
    public DbSet<CarteiraInvestimento> tblCarteiraInvestimentos { get; set; } = null!;
    public DbSet<Investimento> tblInvestimentos { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
