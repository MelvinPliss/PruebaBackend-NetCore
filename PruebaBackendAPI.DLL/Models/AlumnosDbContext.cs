using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using PruebaBackendAPI.DLL.DTOs;

namespace PruebaBackendAPI.DLL.Models;

public partial class AlumnosDbContext : DbContext
{
    public AlumnosDbContext()
    {
    }

    public AlumnosDbContext(DbContextOptions<AlumnosDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<Calificacion> Calificaciones { get; set; }

    public virtual DbSet<Grado> Grados { get; set; }

    public virtual DbSet<Materia> Materias { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public DbSet<CalificacionDTO> CalificacionDTOs { get; set; }
    public DbSet<CalificacionGuardarDTO> CalificacionGuardarDTOs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<CalificacionDTO>().HasNoKey();
        modelBuilder.Entity<CalificacionGuardarDTO>().HasNoKey();

        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.IdAlumno).HasName("PRIMARY");

            entity.ToTable("alumnos");

            entity.HasIndex(e => e.IdGrado, "id_grado");

            entity.Property(e => e.IdAlumno).HasColumnName("id_alumno");
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(100)
                .HasColumnName("apellido_materno");
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(100)
                .HasColumnName("apellido_paterno");
            entity.Property(e => e.Estatus)
                .HasDefaultValueSql("'Activo'")
                .HasColumnType("enum('Activo','Inactivo','Suspendido')")
                .HasColumnName("estatus");
            entity.Property(e => e.FechaDeNacimiento).HasColumnName("fecha_de_nacimiento");
            entity.Property(e => e.Genero)
                .HasColumnType("enum('M','F','O')")
                .HasColumnName("genero");
            entity.Property(e => e.IdGrado).HasColumnName("id_grado");
            entity.Property(e => e.Matricula)
                .HasMaxLength(20)
                .HasColumnName("matricula");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdGradoNavigation).WithMany(p => p.Alumnos)
                .HasForeignKey(d => d.IdGrado)
                .HasConstraintName("alumnos_ibfk_1");
        });

        modelBuilder.Entity<Calificacion>(entity =>
        {
            entity.HasKey(e => e.IdCalificacion).HasName("PRIMARY");

            entity.ToTable("calificaciones");

            entity.HasIndex(e => e.IdGrado, "calificaciones_ibfk_3_idx");

            entity.HasIndex(e => e.IdAlumno, "id_alumno");

            entity.HasIndex(e => e.IdMateria, "id_materia");

            entity.Property(e => e.IdCalificacion).HasColumnName("id_calificacion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.IdAlumno).HasColumnName("id_alumno");
            entity.Property(e => e.IdGrado).HasColumnName("id_grado");
            entity.Property(e => e.IdMateria).HasColumnName("id_materia");
            entity.Property(e => e.Nota)
                .HasPrecision(3, 1)
                .HasColumnName("nota");

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany(p => p.Calificaciones)
                .HasForeignKey(d => d.IdAlumno)
                .HasConstraintName("calificaciones_ibfk_1");

            entity.HasOne(d => d.IdGradoNavigation).WithMany(p => p.Calificaciones)
                .HasForeignKey(d => d.IdGrado)
                .HasConstraintName("calificaciones_ibfk_3");

            entity.HasOne(d => d.IdMateriaNavigation).WithMany(p => p.Calificaciones)
                .HasForeignKey(d => d.IdMateria)
                .HasConstraintName("calificaciones_ibfk_2");
        });

        modelBuilder.Entity<Grado>(entity =>
        {
            entity.HasKey(e => e.IdGrado).HasName("PRIMARY");

            entity.ToTable("grados");

            entity.Property(e => e.IdGrado).HasColumnName("id_grado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Materia>(entity =>
        {
            entity.HasKey(e => e.IdMateria).HasName("PRIMARY");

            entity.ToTable("materias");

            entity.HasIndex(e => e.IdGrado, "materias_ibfk_1_idx");

            entity.Property(e => e.IdMateria).HasColumnName("id_materia");
            entity.Property(e => e.IdGrado).HasColumnName("id_grado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdGradoNavigation).WithMany(p => p.Materia)
                .HasForeignKey(d => d.IdGrado)
                .HasConstraintName("materias_ibfk_1");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Clave)
                .HasMaxLength(100)
                .HasColumnName("clave");
            entity.Property(e => e.Estatus)
                .HasDefaultValueSql("'Activo'")
                .HasColumnType("enum('Activo','Inactivo','Suspendido')")
                .HasColumnName("estatus");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(100)
                .HasColumnName("nombre_usuario");
            entity.Property(e => e.Rol)
                .HasColumnType("enum('Admin','Docente','Alumno')")
                .HasColumnName("rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
