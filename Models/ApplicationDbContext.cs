// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using ReactServer.Mapping;

namespace ReactServer.Models
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<CourseStatus> CourseStatus { get; set; }
        public DbSet<UserCourseInfo> UserCourseInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly= typeof(UserCourseInfoMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }

}
