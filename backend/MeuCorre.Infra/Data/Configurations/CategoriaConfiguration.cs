using MeuCorre.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
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
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(categoria => categoria.Cor)
            .IsRequired()
            .HasMaxLength(25);

        builder.Property(categoria => categoria.Icone)
           .IsRequired()
           .HasMaxLength(6);


        builder.HasIndex(categoria => categoria.Tipo)
           .IsUnique();
    }
}
