using Microsoft.EntityFrameworkCore;
using SchoolApi.Entities;

namespace SchoolApi.Data
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
            modelBuilder.Entity<StudentSubject>()
                .HasKey(ss => new { ss.StudentId, ss.SubjectId });
            modelBuilder.Entity<StudentSubject>()
                .HasOne(s => s.Student)
                .WithMany(ss => ss.StudentSubjects)
                .HasForeignKey(s => s.StudentId);
            modelBuilder.Entity<StudentSubject>()
                .HasOne(s => s.Subject)
                .WithMany(ss => ss.StudentSubjects)
                .HasForeignKey(s => s.SubjectId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
