using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Biblioteca.Mapping
{
    public class AutorMapping : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).HasColumnType("VARCHAR(60)").IsRequired();
            builder.Property(x => x.Sobrenome).HasColumnType("VARCHAR(60)").IsRequired();
            builder.Property(x => x.CriadoEm).HasColumnType("DATETIME").IsRequired();          
            builder.ToTable("autores");
        }
    }
}
