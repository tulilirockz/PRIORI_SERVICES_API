using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Model;
using PRIORI_SERVICES_API.Models;

public class PrioriDbContext : DbContext
{
    public PrioriDbContext(DbContextOptions<PrioriDbContext> options) : base(options) { }

    public DbSet<PostBlog> tblPostBlog { get; set; }
    public DbSet<CategoriaBlog> tblCategoriaBlog { get; set; }
    public DbSet<Consultor> tblConsultores { get; set; }
    public DbSet<Cliente> tblClientes { get; set; }
    public DbSet<Atualizacao> tblAtualizacao { get; set; }
    public DbSet<CarteiraInvestimento> tblCarteiraInvestimentos { get; set; }
    public DbSet<Investimento> tblInvestimentos { get; set; }

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

        modelBuilder.Entity<Atualizacao>().HasKey(p => p.id_atualizacao);
        modelBuilder.Entity<Atualizacao>().Property(p => p.id_consultor).HasColumnType("int");
        modelBuilder.Entity<Atualizacao>().Property(p => p.data_atualizacao).HasColumnType("date");
        modelBuilder.Entity<Atualizacao>().Property(p => p.rentVarAntiga).HasPrecision(8, 2);
        modelBuilder.Entity<Atualizacao>().Property(p => p.rentVarAtual).HasPrecision(8, 2);
        modelBuilder.Entity<Atualizacao>().Property(p => p.rentFixaAtual).HasPrecision(8, 4);
        modelBuilder.Entity<Atualizacao>().Property(p => p.rentFixaAtual).HasPrecision(8, 4);

        modelBuilder.Entity<CarteiraInvestimento>().HasKey(p => p.id_efetuacao);
        modelBuilder.Entity<CarteiraInvestimento>().Property(p => p.rentabilidade_fixa).HasPrecision(8, 4);
        modelBuilder.Entity<CarteiraInvestimento>().Property(p => p.rentabilidade_variavel).HasPrecision(8, 2);
        modelBuilder.Entity<CarteiraInvestimento>().Property(p => p.data_efetuacao).HasColumnType("date");
        modelBuilder.Entity<CarteiraInvestimento>().Property(p => p.valor_aplicado).HasPrecision(8, 2);
        modelBuilder.Entity<CarteiraInvestimento>().Property(p => p.data_encerramento).HasColumnType("date");
        modelBuilder.Entity<CarteiraInvestimento>().Property(p => p.status).HasColumnType("varchar");
        modelBuilder.Entity<CarteiraInvestimento>().Property(p => p.saldo).HasPrecision(8, 2);
        modelBuilder.Entity<CarteiraInvestimento>().Property(p => p.id_cliente_carteira).HasColumnType("int");
        modelBuilder.Entity<CarteiraInvestimento>().Property(p => p.id_investimento).HasColumnType("int");



        modelBuilder.Entity<Investimento>().HasKey(p => p.id_investimento);
        modelBuilder.Entity<Investimento>().Property(p => p.id_riscoInvestimento).HasPrecision(18, 0);
        modelBuilder.Entity<Investimento>().Property(p => p.nome).HasColumnType("varchar");
        modelBuilder.Entity<Investimento>().Property(p => p.tipo_investimento).HasColumnType("varchar");
        modelBuilder.Entity<Investimento>().Property(p => p.rentabilidade_fixa).HasPrecision(8, 4);
        modelBuilder.Entity<Investimento>().Property(p => p.rentabilidade_variavel).HasPrecision(8, 2);
        modelBuilder.Entity<Investimento>().Property(p => p.data_atualizacao).HasColumnType("date");
        modelBuilder.Entity<Investimento>().Property(p => p.vencimento).HasColumnType("date");
        modelBuilder.Entity<Investimento>().Property(p => p.valor_minimo).HasPrecision(8, 2);
        modelBuilder.Entity<Investimento>().Property(p => p.tempo_minimo).HasPrecision(3, 0);

        base.OnModelCreating(modelBuilder);
    }
}