using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using UniversidadDos.Models;

namespace Universidad.Models;

public partial class UniversidadDosContext : DbContext
{
    public UniversidadDosContext()
    {
    }

    public UniversidadDosContext(DbContextOptions<UniversidadDosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<EstudianteMaterium> EstudianteMateria { get; set; }

    public virtual DbSet<Materia> Materias { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__estudian__3213E83F598BDF13");

            entity.ToTable("estudiante");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstudianteMaterium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estudian__3213E83F90015FB9");

            entity.HasIndex(e => new { e.EstuId, e.MateId }, "UQ_Estu_Mate").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nota1).HasColumnName("nota1");
            entity.Property(e => e.Nota2).HasColumnName("nota2");
            entity.Property(e => e.Nota3).HasColumnName("nota3");

            entity.HasOne(d => d.Estu).WithMany(p => p.EstudianteMateria)
                .HasForeignKey(d => d.EstuId)
                .HasConstraintName("fk_estu");

            entity.HasOne(d => d.Mate).WithMany(p => p.EstudianteMateria)
                .HasForeignKey(d => d.MateId)
                .HasConstraintName("fk_mate");
        });

        modelBuilder.Entity<Materia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__materias__3213E83FFAC69559");

            entity.ToTable("materias");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NombreMateria)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
