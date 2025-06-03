
using System;
using GsDotNet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
#nullable disable
namespace GsDotNet.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250530212550_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");
            modelBuilder.Entity("GsDotNet.Models.EventoClimatico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");
                    b.Property<bool>("Ativo")
                        .HasColumnType("INTEGER");
                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("TEXT");
                    b.Property<DateTime>("DataOcorrencia")
                        .HasColumnType("TEXT");
                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");
                    b.Property<string>("Local")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");
                    b.Property<int>("Severidade")
                        .HasColumnType("INTEGER");
                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");
                    b.HasKey("Id");
                    b.ToTable("GsDotNet");
                    b.HasData(
                        new
                        {
                            Id = 1,
                            Ativo = true,
                            DataCadastro = new DateTime(2025, 5, 30, 17, 25, 49, 979, DateTimeKind.Local).AddTicks(2519),
                            DataOcorrencia = new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Descricao = "Enchente causada por chuvas intensas",
                            Local = "São Paulo, SP",
                            Severidade = 4,
                            Tipo = "Enchente"
                        },
                        new
                        {
                            Id = 2,
                            Ativo = true,
                            DataCadastro = new DateTime(2025, 5, 30, 17, 25, 49, 979, DateTimeKind.Local).AddTicks(2530),
                            DataOcorrencia = new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Descricao = "Período prolongado de estiagem",
                            Local = "Fortaleza, CE",
                            Severidade = 3,
                            Tipo = "Seca"
                        });
                });
            modelBuilder.Entity("GsDotNet.Models.PessoaAfetada", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");
                    b.Property<bool>("Ativo")
                        .HasColumnType("INTEGER");
                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("TEXT");
                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("TEXT");
                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("TEXT");
                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");
                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");
                    b.Property<int>("EventoClimaticoId")
                        .HasColumnType("INTEGER");
                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");
                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");
                    b.Property<string>("TipoAfetacao")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");
                    b.HasKey("Id");
                    b.HasIndex("CPF")
                        .IsUnique();
                    b.HasIndex("EventoClimaticoId");
                    b.ToTable("PessoasAfetadas");
                    b.HasData(
                        new
                        {
                            Id = 1,
                            Ativo = true,
                            CPF = "12345678901",
                            DataCadastro = new DateTime(2025, 5, 30, 17, 25, 49, 979, DateTimeKind.Local).AddTicks(2701),
                            DataNascimento = new DateTime(1985, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "joao.silva@email.com",
                            Endereco = "Rua das Flores, 123 - São Paulo, SP",
                            EventoClimaticoId = 1,
                            Nome = "João Silva",
                            Telefone = "(11) 98765-4321",
                            TipoAfetacao = "Desabrigado"
                        },
                        new
                        {
                            Id = 2,
                            Ativo = true,
                            CPF = "98765432101",
                            DataCadastro = new DateTime(2025, 5, 30, 17, 25, 49, 979, DateTimeKind.Local).AddTicks(2710),
                            DataNascimento = new DateTime(1990, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "maria.oliveira@email.com",
                            Endereco = "Av. Central, 456 - Fortaleza, CE",
                            EventoClimaticoId = 2,
                            Nome = "Maria Oliveira",
                            Telefone = "(85) 91234-5678",
                            TipoAfetacao = "Perda de plantação"
                        });
                });
            modelBuilder.Entity("GsDotNet.Models.PessoaAfetada", b =>
                {
                    b.HasOne("GsDotNet.Models.EventoClimatico", "EventoClimatico")
                        .WithMany("PessoasAfetadas")
                        .HasForeignKey("EventoClimaticoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                    b.Navigation("EventoClimatico");
                });
            modelBuilder.Entity("GsDotNet.Models.EventoClimatico", b =>
                {
                    b.Navigation("PessoasAfetadas");
                });
#pragma warning restore 612, 618
        }
    }
}
