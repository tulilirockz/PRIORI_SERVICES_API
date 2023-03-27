using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Model;

public class PrioriDbContext : DbContext
{
    public PrioriDbContext(DbContextOptions<PrioriDbContext> options) : base(options) { }

    public DbSet<PostBlog> tblPostBlog { get; set; }
    public DbSet<CategoriaBlog> tblCategoriaBlog { get; set; }
    public DbSet<Consultor> tblConsultores { get; set; }
    public DbSet<Cliente> tblClientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PostBlog>().HasKey(p => p.id_post);
        modelBuilder.Entity<PostBlog>().Property(p => p.id_autor).HasColumnType("int").HasMaxLength(1);
        modelBuilder.Entity<PostBlog>().Property(p => p.id_categoria).HasColumnType("int").HasMaxLength(1);
        modelBuilder.Entity<PostBlog>().Property(p => p.data_criacao).HasColumnType("datetime");
        modelBuilder.Entity<PostBlog>().Property(p => p.titulo).HasColumnType("varchar").HasMaxLength(50);
        modelBuilder.Entity<PostBlog>().Property(p => p.descricao).HasColumnType("varchar").HasMaxLength(200);
        modelBuilder.Entity<PostBlog>().Property(p => p.conteudo).HasColumnType("varchar").HasMaxLength(2000);

        modelBuilder.Entity<CategoriaBlog>().HasKey(p => p.id_categoria);
        modelBuilder.Entity<CategoriaBlog>().Property(p => p.nome_categoria).HasColumnType("varchar").HasMaxLength(50);

        modelBuilder.Entity<Cliente>().HasKey(p => p.id_cliente);
        modelBuilder.Entity<Cliente>().Property(p => p.nome).HasColumnType("varchar").HasMaxLength(40);
        modelBuilder.Entity<Cliente>().Property(p => p.id_consultor).HasColumnType("int");
        modelBuilder.Entity<Cliente>().Property(p => p.id_tipoinvestidor).HasColumnType("int");
        modelBuilder.Entity<Cliente>().Property(p => p.cpf).HasColumnType("varchar").HasMaxLength(11);
        modelBuilder.Entity<Cliente>().Property(p => p.email).HasColumnType("varchar").HasMaxLength(25);
        modelBuilder.Entity<Cliente>().Property(p => p.pontuacao).HasColumnType("numeric");
        modelBuilder.Entity<Cliente>().Property(p => p.endereco).HasColumnType("varchar").HasMaxLength(60);
        modelBuilder.Entity<Cliente>().Property(p => p.telefone).HasColumnType("char").HasMaxLength(15);
        modelBuilder.Entity<Cliente>().Property(p => p.senha).HasColumnType("char").HasMaxLength(15);
        modelBuilder.Entity<Cliente>().Property(p => p.senhaHash).HasColumnType("varchar").HasMaxLength(200);
        modelBuilder.Entity<Cliente>().Property(p => p.senhaSalt).HasColumnType("varchar").HasMaxLength(200);

        modelBuilder.Entity<Consultor>().HasKey(p => p.id_consultor);
        modelBuilder.Entity<Consultor>().Property(p => p.nome).HasColumnType("varchar").HasMaxLength(40);
        modelBuilder.Entity<Consultor>().Property(p => p.cpf).HasColumnType("varchar").HasMaxLength(11);
        modelBuilder.Entity<Consultor>().Property(p => p.email).HasColumnType("varchar").HasMaxLength(100);
        modelBuilder.Entity<Consultor>().Property(p => p.telefone).HasColumnType("char").HasMaxLength(15);
        modelBuilder.Entity<Consultor>().Property(p => p.data_contratacao).HasColumnType("date");
        modelBuilder.Entity<Consultor>().Property(p => p.data_demissao).HasColumnType("date");
        modelBuilder.Entity<Consultor>().Property(p => p.status).HasColumnType("varchar").HasMaxLength(8);
        modelBuilder.Entity<Consultor>().Property(p => p.usuario).HasColumnType("varchar").HasMaxLength(50);
        modelBuilder.Entity<Consultor>().Property(p => p.senha).HasColumnType("varchar").HasMaxLength(100);
        modelBuilder.Entity<Consultor>().Property(p => p.senhaHash).HasColumnType("varchar").HasMaxLength(200);
        modelBuilder.Entity<Consultor>().Property(p => p.senhaSalt).HasColumnType("varchar").HasMaxLength(200);

        base.OnModelCreating(modelBuilder);
    }
}