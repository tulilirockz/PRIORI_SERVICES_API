using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Model;

public class PrioriDbContext : DbContext
{
    public PrioriDbContext(DbContextOptions<PrioriDbContext> options) : base(options) { }

    public DbSet<PostBlog> tblPostBlog { get; set; }
    public DbSet<CategoriaBlog> tblCategoriaBlog { get; set; }
    // public DbSet<tblPostBlog> tblConsultor { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PostBlog>().HasKey(p => p.id_post);
        modelBuilder.Entity<PostBlog>().Property(p => p.id_autor).HasColumnType("int").HasMaxLength(1);
        modelBuilder.Entity<PostBlog>().Property(p => p.id_categoria).HasColumnType("int").HasMaxLength(1);
        modelBuilder.Entity<PostBlog>().Property(p => p.data_criacao).HasColumnType("datetime");
        modelBuilder.Entity<PostBlog>().Property(p => p.titulo).HasColumnType("varchar").HasMaxLength(50);
        modelBuilder.Entity<PostBlog>().Property(p => p.conteudo).HasColumnType("varchar").HasMaxLength(2000);

        modelBuilder.Entity<CategoriaBlog>().HasKey(p => p.id_categoria);
        modelBuilder.Entity<CategoriaBlog>().Property(p => p.nome_categoria).HasColumnType("varchar").HasMaxLength(50);

        base.OnModelCreating(modelBuilder);
    }
}