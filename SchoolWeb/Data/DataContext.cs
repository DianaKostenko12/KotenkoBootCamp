using Microsoft.EntityFrameworkCore;
using SchoolWeb.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SchoolWeb.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentSubject>(builder =>
            {
                builder
                    .HasKey(ss => new { ss.StudentId, ss.SubjectId });
                builder
                    .HasOne(s => s.Student)
                    .WithMany(ss => ss.StudentSubjects);
                builder
                    .HasOne(s => s.Subject)
                    .WithMany(ss => ss.StudentSubjects)
                    .HasForeignKey(s => s.SubjectId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
