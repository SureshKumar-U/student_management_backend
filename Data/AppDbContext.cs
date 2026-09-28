namespace CrudAPi.Data;
using Microsoft.EntityFrameworkCore;
using CrudAPi.Models;
using StudentManagement.Models;

public class AppDbContext:DbContext{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // This creates a table named "Students" in your database
        public DbSet<User> Users => Set<User>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Course> Courses => Set<Course>();

        public DbSet<Enrollment> Enrollments => Set<Enrollment>();


        public DbSet<Department> Departments => Set<Department>();

}