using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace RE005.DB
{
    public partial class RE005Context : DbContext
    {
        public RE005Context()
        {
        }

        public RE005Context(DbContextOptions<RE005Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Field> Fields { get; set; }
        public virtual DbSet<FieldsByFlowExec> FieldsByFlowExecs { get; set; }
        public virtual DbSet<FieldsByStep> FieldsBySteps { get; set; }
        public virtual DbSet<Flow> Flows { get; set; }
        public virtual DbSet<FlowsExec> FlowsExecs { get; set; }
        public virtual DbSet<Step> Steps { get; set; }
        public virtual DbSet<StepsByFlow> StepsByFlows { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Field>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FieldsByFlowExec>(entity =>
            {
                entity.ToTable("FieldsByFlowExec");

                entity.Property(e => e.RecordDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Value).IsUnicode(false);

                entity.HasOne(d => d.IdFieldNavigation)
                    .WithMany(p => p.FieldsByFlowExecs)
                    .HasForeignKey(d => d.IdField)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FieldsByF__IdFie__01142BA1");

                entity.HasOne(d => d.IdFlowExecNavigation)
                    .WithMany(p => p.FieldsByFlowExecs)
                    .HasForeignKey(d => d.IdFlowExec)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FieldsByF__IdFlo__00200768");
            });

            modelBuilder.Entity<FieldsByStep>(entity =>
            {
                entity.ToTable("FieldsByStep");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdFieldNavigation)
                    .WithMany(p => p.FieldsBySteps)
                    .HasForeignKey(d => d.IdField)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FieldsByS__IdFie__73BA3083");

                entity.HasOne(d => d.IdStepNavigation)
                    .WithMany(p => p.FieldsBySteps)
                    .HasForeignKey(d => d.IdStep)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FieldsByS__IdSte__72C60C4A");
            });

            modelBuilder.Entity<Flow>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FlowsExec>(entity =>
            {
                entity.ToTable("FlowsExec");

                entity.Property(e => e.RecordDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdFlowNavigation)
                    .WithMany(p => p.FlowsExecs)
                    .HasForeignKey(d => d.IdFlow)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FlowsExec__IdFlo__7C4F7684");
            });

            modelBuilder.Entity<Step>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Service)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StepsByFlow>(entity =>
            {
                entity.ToTable("StepsByFlow");

                entity.HasOne(d => d.IdFlowNavigation)
                    .WithMany(p => p.StepsByFlows)
                    .HasForeignKey(d => d.IdFlow)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StepsByFl__IdFlo__778AC167");

                entity.HasOne(d => d.IdStepNavigation)
                    .WithMany(p => p.StepsByFlows)
                    .HasForeignKey(d => d.IdStep)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StepsByFl__IdSte__787EE5A0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
