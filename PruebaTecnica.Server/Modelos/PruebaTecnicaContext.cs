using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PruebaTecnica.Server.Modelos
{
    public partial class PruebaTecnicaContext : DbContext
    {
        public PruebaTecnicaContext()
        {
        }

        public PruebaTecnicaContext(DbContextOptions<PruebaTecnicaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Estudiante> Estudiantes { get; set; } = null!;
        public virtual DbSet<EstudianteMaterium> EstudianteMateria { get; set; } = null!;
        public virtual DbSet<Materium> Materia { get; set; } = null!;
        public virtual DbSet<Profesor> Profesors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                    .AddJsonFile("appsettings.json")
                                    .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("SQL"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.ToTable("Estudiante");

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<EstudianteMaterium>(entity =>
            {
                entity.HasOne(d => d.Estudiante)
                    .WithMany(p => p.EstudianteMateria)
                    .HasForeignKey(d => d.EstudianteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Estudiant__Estud__3E52440B");

                entity.HasOne(d => d.Materia)
                    .WithMany(p => p.EstudianteMateria)
                    .HasForeignKey(d => d.MateriaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Estudiant__Mater__3F466844");
            });

            modelBuilder.Entity<Materium>(entity =>
            {
                entity.Property(e => e.Creditos).HasDefaultValueSql("((3))");

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<Profesor>(entity =>
            {
                entity.ToTable("Profesor");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.HasMany(d => d.Materia)
                    .WithMany(p => p.Profesors)
                    .UsingEntity<Dictionary<string, object>>(
                        "ProfesorMaterium",
                        l => l.HasOne<Materium>().WithMany().HasForeignKey("MateriaId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__ProfesorM__Mater__4316F928"),
                        r => r.HasOne<Profesor>().WithMany().HasForeignKey("ProfesorId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__ProfesorM__Profe__4222D4EF"),
                        j =>
                        {
                            j.HasKey("ProfesorId", "MateriaId").HasName("PK__Profesor__4D23E9163CD42D1B");

                            j.ToTable("ProfesorMateria");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
