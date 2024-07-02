

using GestaoTarefas.Domain.Entities.Comum;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GestaoTarefas.Data.Mapping
{
    public class ListaMap : IEntityTypeConfiguration<ListaEntity>
    {
        public void Configure(EntityTypeBuilder<ListaEntity> builder)
        {
            builder.ToTable("Lista");

            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.NomeLista).IsUnique();

            builder.Property(p => p.Id).HasColumnName("IdLista");

            builder.Property(p => p.NomeLista).HasColumnName("NomeLista").IsRequired().HasMaxLength(60); ;

            builder.Property(p => p.NomeAutor).HasColumnName("NomeAutor").IsRequired().HasMaxLength(60); ;

            builder.Property(p => p.DataCriacao).HasColumnName("DataCriacao");

        }
    }
}


