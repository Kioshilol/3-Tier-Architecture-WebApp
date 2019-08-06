using DLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DLayer.EFContext.EfEntities
{
    public partial class TrainingTaskContext : DbContext
    {
        public TrainingTaskContext()
        {
        }

        public TrainingTaskContext(DbContextOptions<TrainingTaskContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeTasks> EmployeeTasks { get; set; }
        public virtual DbSet<Task> Task { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=QWS-KHP-10\\SQLEXPRESS;Database=TrainingTask;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.ShortName).HasMaxLength(50);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Position).HasMaxLength(50);

                entity.Property(e => e.SecondName).HasMaxLength(50);

                entity.Property(e => e.Surname).HasMaxLength(50);
            });

            modelBuilder.Entity<EmployeeTasks>(entity =>
            {
                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeTasks)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_StaffTasks_Staff")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.EmployeeTasks)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_StaffTasks_Task")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(e => e.DateOfEnd).HasColumnType("date");

                entity.Property(e => e.DateOfStart).HasColumnType("date");

                entity.Property(e => e.TaskTime).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.TypeStatus).HasMaxLength(50);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_Task_Project")
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
