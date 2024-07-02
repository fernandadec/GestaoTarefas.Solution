using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GestaoTarefas.Domain.Entities.Comum;

namespace GestaoTarefas.Data.Mapping
{
    public class ListaTarefasMap : IEntityTypeConfiguration<ListaTarefasEntity>
    {
        public void Configure(EntityTypeBuilder<ListaTarefasEntity> builder)
        {
            builder.ToTable("ListaTarefas");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("IdListaTarefas");
            builder.Property(li => li.ListaId).HasColumnName("IdLista").IsRequired();

            builder.Property(p => p.Titulo).HasColumnName("Titulo").IsRequired().HasMaxLength(60);

            builder.Property(p => p.Descricao).HasColumnName("Descricao").IsRequired().HasMaxLength(250);

            builder.Property(p => p.DataCriacao).HasColumnName("DataCriacao");

            builder.Property(p => p.Ativo).HasColumnName("Ativo");


            builder.HasOne(l => l.Listas)
                .WithMany(l => l.ListaTarefas)
                .HasForeignKey(l => l.ListaId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}