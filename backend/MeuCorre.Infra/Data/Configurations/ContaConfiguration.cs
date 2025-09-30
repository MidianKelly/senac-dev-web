using MeuCorre.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuCorre.Infra.Data.Configurations
{
    class ContaConfiguration : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {

            /// Define o nome da tabela no banco de dados.
            builder.ToTable("Conta");

            //Define a chave primária.
            builder.HasKey(conta => conta.Id);

            //Define as propriedades e suas configurações.
            builder.Property(conta => conta.Nome)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(conta => conta.TipoConta)
                .IsRequired();

            builder.Property(conta => conta.TipoLimite)
                .IsRequired();

            builder.Property(conta => conta.Saldo)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasOne(conta => conta.Usuario)
                .WithMany(usuario => usuario.Conta)
                .HasForeignKey(conta => conta.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);



        }
    }
}
