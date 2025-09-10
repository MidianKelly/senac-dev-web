using MeuCorre.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuCorre.Infra.Data.Configurations
{
    internal class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            // Define o nome da tabela
            builder.ToTable("Categoria");

            // Define a chave primária
            builder.HasKey(categoria => categoria.Id);

            // Configura propriedades
            builder.Property(categoria => categoria.Nome)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(categoria => categoria.Descricao)
                .HasMaxLength(200);

            builder.Property(categoria => categoria.Cor)
                .HasMaxLength(10);

            builder.Property(categoria => categoria.Icone)
               .HasMaxLength(10);


            builder.Property(categoria => categoria.Tipo)
             .IsRequired();

            builder.Property(categoria => categoria.UsuarioId)
             .IsRequired();

            builder.Property(categoria => categoria.DataCriacao)
              .IsRequired();

            builder.Property(categoria => categoria.DataAtualizacao)
              .IsRequired(false); ///FALSE É PARA ESPECIFICAR QUE O CAMPO PODE SER OPCIONAL

            //Chaves Estrangeiras FK
            //Define o relacionamento entre a Categoria e o Usuario
            builder.HasOne(categoria => categoria.Usuario)
                .WithMany(usuario => usuario.Categorias)
                .HasForeignKey(categoria => categoria.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade); // Define o comportamento de exclusão em cascata
        }
    }
}

