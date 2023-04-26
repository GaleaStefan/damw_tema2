using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class AppDbContext : DbContext
    {
        #region Properties and Indexers
        public DbSet<Class> Classes { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion

        #region Protected members
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=.\\SQLExpress;Database=LabProject;User Id=admin;Password=admin;")
                .LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Class>().Property(e => e.Name).HasMaxLength(10);
            modelBuilder.Entity<Grade>().HasOne(e => e.Class)
                .WithMany()
                .HasForeignKey(e => e.ClassId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Grade>().HasOne(e => e.Student)
                .WithMany()
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Class>().HasOne(e => e.Teacher)
                .WithMany()
                .HasForeignKey(e => e.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        #endregion
    }
}
