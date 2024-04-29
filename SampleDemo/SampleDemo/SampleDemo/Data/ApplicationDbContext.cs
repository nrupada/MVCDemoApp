using Microsoft.EntityFrameworkCore;
using SampleDemo.Entities;

namespace SampleDemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<ClassroomStudent> ClassroomStudents { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Classroom
            modelBuilder.Entity<Classroom>().HasData(new Classroom { ClassroomId = 1, Name = "Math A" });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { ClassroomId = 2, Name = "Math B" });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { ClassroomId = 3, Name = "Math C" });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { ClassroomId = 4, Name = "Math D" });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { ClassroomId = 5, Name = "Math E" });
            #endregion

            #region Student
            modelBuilder.Entity<Student>().HasData(new Student { StudentId = 1, Name = "Bob" });
            modelBuilder.Entity<Student>().HasData(new Student { StudentId = 2, Name = "James" });
            modelBuilder.Entity<Student>().HasData(new Student { StudentId = 3, Name = "Beth" });
            modelBuilder.Entity<Student>().HasData(new Student { StudentId = 4, Name = "Sam" });
            modelBuilder.Entity<Student>().HasData(new Student { StudentId = 5, Name = "Jen" });
            #endregion

            #region ClassroomStudent
            modelBuilder.Entity<ClassroomStudent>().HasData(new ClassroomStudent { ClassroomStudentId = 1, StudentId = 1, ClassroomId = 1 });
            modelBuilder.Entity<ClassroomStudent>().HasData(new ClassroomStudent { ClassroomStudentId = 2, StudentId = 2, ClassroomId = 1 });
            modelBuilder.Entity<ClassroomStudent>().HasData(new ClassroomStudent { ClassroomStudentId = 3, StudentId = 3, ClassroomId = 1 });
            modelBuilder.Entity<ClassroomStudent>().HasData(new ClassroomStudent { ClassroomStudentId = 4, StudentId = 4, ClassroomId = 1 });
            modelBuilder.Entity<ClassroomStudent>().HasData(new ClassroomStudent { ClassroomStudentId = 5, StudentId = 5, ClassroomId = 1 });
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}