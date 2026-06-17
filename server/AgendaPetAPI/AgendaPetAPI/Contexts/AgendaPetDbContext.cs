using System;
using System.Collections.Generic;
using AgendaPetAPI.Domains;
using Microsoft.EntityFrameworkCore;

namespace AgendaPetAPI.Contexts;

public partial class AgendaPetDbContext : DbContext
{
    public AgendaPetDbContext()
    {
    }

    public AgendaPetDbContext(DbContextOptions<AgendaPetDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agendamento> Agendamento { get; set; }

    public virtual DbSet<ComportamentoPet> ComportamentoPet { get; set; }

    public virtual DbSet<LogAgendamento> LogAgendamento { get; set; }

    public virtual DbSet<Pet> Pet { get; set; }

    public virtual DbSet<PortePet> PortePet { get; set; }

    public virtual DbSet<RacaPet> RacaPet { get; set; }

    public virtual DbSet<Servico> Servico { get; set; }

    public virtual DbSet<StatusAgendamento> StatusAgendamento { get; set; }

    public virtual DbSet<TipoAnimal> TipoAnimal { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=AgendaPetDb; Trusted_Connection=true; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agendamento>(entity =>
        {
            entity.HasKey(e => e.AgendamentoID).HasName("PK__Agendame__AE0131135AE982BE");

            entity.Property(e => e.AgendamentoID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ValorTotal).HasColumnType("decimal(6, 2)");

            entity.HasOne(d => d.Funcionario).WithMany(p => p.Agendamento)
                .HasForeignKey(d => d.FuncionarioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Agendamento_Funcionario_FuncionarioID");

            entity.HasOne(d => d.Pet).WithMany(p => p.Agendamento)
                .HasForeignKey(d => d.PetID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Agendamento_Pet_PetID");

            entity.HasOne(d => d.StatusAgendamento).WithMany(p => p.Agendamento)
                .HasForeignKey(d => d.StatusAgendamentoID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Agendamento_StatusAgendamento_StatusAgendamentoID");
        });

        modelBuilder.Entity<ComportamentoPet>(entity =>
        {
            entity.HasKey(e => e.ComportamentoID).HasName("PK__Comporta__DDE19EC9DE463394");

            entity.Property(e => e.ComportamentoID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeComportamento)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LogAgendamento>(entity =>
        {
            entity.HasKey(e => e.LogAgendamentoID).HasName("PK__LogAgend__2D9A4EF450AECCE9");

            entity.Property(e => e.LogAgendamentoID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DataAnteriorAgendameto).HasColumnType("datetime");
            entity.Property(e => e.DataModificacao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.StatusAgendamentoAnterior).HasMaxLength(30);

            entity.HasOne(d => d.Agendamento).WithMany(p => p.LogAgendamento)
                .HasForeignKey(d => d.AgendamentoID)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Pet>(entity =>
        {
            entity.HasKey(e => e.PetID).HasName("PK__Pet__48E53802F2F81528");

            entity.Property(e => e.PetID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Comportamento).WithMany(p => p.Pet)
                .HasForeignKey(d => d.ComportamentoID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pet_Comportamento_ComportamentoID");

            entity.HasOne(d => d.Porte).WithMany(p => p.Pet)
                .HasForeignKey(d => d.PorteID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pet_Porte_PorteID");

            entity.HasOne(d => d.Raca).WithMany(p => p.Pet)
                .HasForeignKey(d => d.RacaID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pet_Raca_RacaID");

            entity.HasOne(d => d.TipoAnimal).WithMany(p => p.Pet)
                .HasForeignKey(d => d.TipoAnimalID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Usuario).WithMany(p => p.Pet)
                .HasForeignKey(d => d.UsuarioID)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<PortePet>(entity =>
        {
            entity.HasKey(e => e.PorteID).HasName("PK__PortePet__DD930486A0349144");

            entity.Property(e => e.PorteID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomePorte)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RacaPet>(entity =>
        {
            entity.HasKey(e => e.RacaID).HasName("PK__RacaPet__06FBD6D21E047D93");

            entity.Property(e => e.RacaID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeRaca)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Servico>(entity =>
        {
            entity.HasKey(e => e.ServicoID).HasName("PK__Servico__C597679690AA3E7F");

            entity.Property(e => e.ServicoID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeServico)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Preco).HasColumnType("decimal(6, 2)");

            entity.HasMany(d => d.Agendamento).WithMany(p => p.Servico)
                .UsingEntity<Dictionary<string, object>>(
                    "AgendamentoServico",
                    r => r.HasOne<Agendamento>().WithMany()
                        .HasForeignKey("AgendamentoID")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Servico>().WithMany()
                        .HasForeignKey("ServicoID")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("ServicoID", "AgendamentoID").HasName("PK_AgendamentoServico_ServicoID_AgendamentoID");
                    });
        });

        modelBuilder.Entity<StatusAgendamento>(entity =>
        {
            entity.HasKey(e => e.StatusAgendamentoID).HasName("PK__StatusAg__0C862FF92199AB89");

            entity.Property(e => e.StatusAgendamentoID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeStatus)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoAnimal>(entity =>
        {
            entity.HasKey(e => e.TipoAnimalID).HasName("PK__TipoAnim__70437777C5E7271D");

            entity.Property(e => e.TipoAnimalID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeTipo)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.TipoUsuarioID).HasName("PK__TipoUsua__7F22C702FC902BD8");

            entity.Property(e => e.TipoUsuarioID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeTipo)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioID).HasName("PK__Usuario__2B3DE798881A4B90");

            entity.HasIndex(e => e.NumeroTelefone, "UQ__Usuario__0DC3DBFFBAC6014B").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D10534A2D653CB").IsUnique();

            entity.Property(e => e.UsuarioID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.NumeroTelefone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(32);
            entity.Property(e => e.StatusUsuarioID).HasDefaultValue(true);

            entity.HasOne(d => d.TipoUsuario).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.TipoUsuarioID)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
