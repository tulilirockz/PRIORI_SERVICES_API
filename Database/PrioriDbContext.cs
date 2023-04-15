using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Model;
using PRIORI_SERVICES_API.Models;

public class PrioriDbContext : DbContext
{
    public PrioriDbContext(DbContextOptions<PrioriDbContext> options) : base(options)
    {
    }

    public DbSet<PostBlog> tblPostBlog { get; set; }
    public DbSet<CategoriaBlog> tblCategoriaBlog { get; set; }
    public DbSet<Consultor> tblConsultores { get; set; }
    public DbSet<Cliente> tblClientes { get; set; }
    public DbSet<Atualizacao> tblAtualizacao { get; set; }
    public DbSet<CarteiraInvestimento> tblCarteiraInvestimentos { get; set; }
    public DbSet<Investimento> tblInvestimentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
