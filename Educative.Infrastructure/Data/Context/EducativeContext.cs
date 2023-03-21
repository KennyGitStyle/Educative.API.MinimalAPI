using Educative.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace Educative.Infrastructure.Data.Context
{
    public class EducativeContext : DbContext
    {
        public EducativeContext(DbContextOptions options) :
            base(options)
        {
            
        }
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<StudentCourse> StudentCourses { get; set; } = null!; 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<StudentCourse>()
            .HasKey(sc => new { sc.StudentID, sc.CourseID });

            modelBuilder.Entity<StudentCourse>()
            .HasOne(sc => sc.Student)
            .WithMany(s => s.StudentCourses)
            .HasForeignKey(sc => sc.StudentID);

            modelBuilder.Entity<StudentCourse>()
            .HasOne(sc => sc.Course)
            .WithMany(s => s.StudentCourses)
            .HasForeignKey(sc => sc.CourseID);

            modelBuilder.Entity<Student>()
            .HasOne(a => a.Address)
            .WithOne(s => s.Student);

            if(Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));

                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }
                }
            } 
        }
    }
}