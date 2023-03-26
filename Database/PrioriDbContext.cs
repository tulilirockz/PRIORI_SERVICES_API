using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Model;

public class PrioriDbContext : DbContext
{
    public PrioriDbContext(DbContextOptions<PrioriDbContext> options) : base(options) { }

    public DbSet<PostBlog> tblPostBlog { get; set; }
    public DbSet<CategoriaBlog> tblCategoriaBlog { get; set; }
    public DbSet<Consultor> tblConsultor { get; set; }
    public DbSet<Usuario> tblUsuario { get; set; }

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

        modelBuilder.Entity<Usuario>().HasKey(p => p.id);
        modelBuilder.Entity<Usuario>().Property(p => p.nome).HasColumnType("varchar").HasMaxLength(40);
        modelBuilder.Entity<Usuario>().Property(p => p.id_consultor).HasColumnType("int");
        modelBuilder.Entity<Usuario>().Property(p => p.id_tipoinvestidor).HasColumnType("int");
        modelBuilder.Entity<Usuario>().Property(p => p.cpf).HasColumnType("varchar").HasMaxLength(11);
        modelBuilder.Entity<Usuario>().Property(p => p.email).HasColumnType("varchar").HasMaxLength(25);
        modelBuilder.Entity<Usuario>().Property(p => p.pontuacao).HasColumnType("numeric");
        modelBuilder.Entity<Usuario>().Property(p => p.endereco).HasColumnType("varchar").HasMaxLength(60);
        modelBuilder.Entity<Usuario>().Property(p => p.telefone).HasColumnType("char").HasMaxLength(15);
        modelBuilder.Entity<Usuario>().Property(p => p.telefone).HasColumnType("char").HasMaxLength(15);

        modelBuilder.Entity<Consultor>().HasKey(p => p.id);
        modelBuilder.Entity<Consultor>().Property(p => p.nome).HasColumnType("varchar").HasMaxLength(40);
        modelBuilder.Entity<Consultor>().Property(p => p.cpf).HasColumnType("varchar").HasMaxLength(11);
        modelBuilder.Entity<Consultor>().Property(p => p.email).HasColumnType("varchar").HasMaxLength(100);
        modelBuilder.Entity<Consultor>().Property(p => p.telefone).HasColumnType("char").HasMaxLength(15);
        modelBuilder.Entity<Consultor>().Property(p => p.data_criacao).HasColumnType("date");
        modelBuilder.Entity<Consultor>().Property(p => p.data_demissao).HasColumnType("date");
        modelBuilder.Entity<Consultor>().Property(p => p.status).HasColumnType("varchar").HasMaxLength(8);
        modelBuilder.Entity<Consultor>().Property(p => p.usuario).HasColumnType("varchar").HasMaxLength(50);
        modelBuilder.Entity<Consultor>().Property(p => p.senha).HasColumnType("varchar").HasMaxLength(100);

        base.OnModelCreating(modelBuilder);
    }
}