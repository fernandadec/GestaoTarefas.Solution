﻿using GestaoTarefas.Domain.Entities.Comum;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefas.Data.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<UsuarioEntity>
    {
        public void Configure(EntityTypeBuilder<UsuarioEntity> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Email).IsUnique();

            builder.HasIndex(p => new {
                p.Email,
                p.Senha
            });
            builder.Property(p => p.Id).HasColumnName("IdUsuario");

            builder.Property(p => p.Email).IsRequired().HasMaxLength(150);

            builder.Property(p => p.Senha).IsRequired();

            builder.Property(p => p.Nome).IsRequired().HasMaxLength(60);

            builder.Property(p => p.Ativo).IsRequired();

            builder.Property(p => p.Token).HasColumnName("Token");

        }
    }
}

