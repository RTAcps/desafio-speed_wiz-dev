using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Biblioteca.Mapping
{
    public class LivroMapping : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Descricao).HasColumnType("VARCHAR(60)").IsRequired();
            builder.Property(x => x.ISBN).IsRequired();
            builder.Property(x => x.AnoLancamento).IsRequired();
            builder.Property(x => x.CriadoEm).HasColumnType("DATETIME").IsRequired();
            builder.HasOne(x => x.Autor)
                .WithMany()
                .HasForeignKey(x => x.AutorId);
            builder.ToTable("livros");
        }
    }
}
