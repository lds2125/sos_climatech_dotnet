using GsDotNet.Models;
using Microsoft.EntityFrameworkCore;
using System;
namespace GsDotNet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<EventoClimatico> GsDotNet { get; set; }
        public DbSet<PessoaAfetada> PessoasAfetadas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<EventoClimatico>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Tipo).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Descricao).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Local).IsRequired().HasMaxLength(100);
                entity.HasMany(e => e.PessoasAfetadas)
                      .WithOne(p => p.EventoClimatico)
                      .HasForeignKey(p => p.EventoClimaticoId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<PessoaAfetada>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CPF).IsRequired().HasMaxLength(11);
                entity.HasIndex(e => e.CPF).IsUnique();
                entity.Property(e => e.Endereco).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Telefone).HasMaxLength(20);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.TipoAfetacao).IsRequired().HasMaxLength(50);
            });
            SeedData(modelBuilder);
        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventoClimatico>().HasData(
                new EventoClimatico
                {
                    Id = 1,
                    Tipo = "Enchente",
                    Descricao = "Enchente causada por chuvas intensas",
                    DataOcorrencia = new DateTime(2025, 3, 15),
                    Local = "São Paulo, SP",
                    Severidade = 4,
                    DataCadastro = DateTime.Now,
                    Ativo = true
                },
                new EventoClimatico
                {
                    Id = 2,
                    Tipo = "Seca",
                    Descricao = "Período prolongado de estiagem",
                    DataOcorrencia = new DateTime(2025, 1, 10),
                    Local = "Fortaleza, CE",
                    Severidade = 3,
                    DataCadastro = DateTime.Now,
                    Ativo = true
                }
            );
            modelBuilder.Entity<PessoaAfetada>().HasData(
                new PessoaAfetada
                {
                    Id = 1,
                    Nome = "João Silva",
                    CPF = "12345678901",
                    DataNascimento = new DateTime(1985, 5, 20),
                    Endereco = "Rua das Flores, 123 - São Paulo, SP",
                    Telefone = "(11) 98765-4321",
                    Email = "joao.silva@email.com",
                    TipoAfetacao = "Desabrigado",
                    EventoClimaticoId = 1,
                    DataCadastro = DateTime.Now,
                    Ativo = true
                },
                new PessoaAfetada
                {
                    Id = 2,
                    Nome = "Maria Oliveira",
                    CPF = "98765432101",
                    DataNascimento = new DateTime(1990, 8, 15),
                    Endereco = "Av. Central, 456 - Fortaleza, CE",
                    Telefone = "(85) 91234-5678",
                    Email = "maria.oliveira@email.com",
                    TipoAfetacao = "Perda de plantação",
                    EventoClimaticoId = 2,
                    DataCadastro = DateTime.Now,
                    Ativo = true
                }
            );
        }
    }
}
