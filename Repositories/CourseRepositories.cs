


using CrudAPi.Data;
using CrudAPi.Dtos;
using CrudAPi.Exceptions;
using CrudAPi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;



public interface ICourseRepository
{
    Task CreateCourse(Course course);
    Task UpdateCourse(Course course);
    Task<Course> GetCourseById(Guid id);
    Task<List<CourseResponseDto>> GetAllCourses();
    Task DeleteCourse(Course course);

     Task<List<Course>>  GetByIdsAsync( List<Guid> courseIds);



}


public class CourseRepository : ICourseRepository
{
    public readonly AppDbContext db;
    public CourseRepository(AppDbContext dbContext)
    {
        db = dbContext;

    }

    public async Task CreateCourse(Course course)

    {
        await db.Courses.AddAsync(course);
        await db.SaveChangesAsync();
    }

    public async Task<Course> GetCourseById(Guid id)
    {
        return await db.Courses.FindAsync(id);
    }

    public async Task<List<CourseResponseDto>> GetAllCourses()
    {
        List<CourseResponseDto> courses = await db.Courses
     .Select(c => new CourseResponseDto
     {
         Id = c.Id,
         Code = c.Code,
         Name = c.Name,
         DepartmentId = c.DepartmentId,
         DepartmentName = c.Department.Name
     })
     .ToListAsync();

        return courses;
    }

    public async Task UpdateCourse(Course course)
    {
        db.Courses.Update(course);
        await db.SaveChangesAsync();
    }

        public async Task<List<Course>> GetByIdsAsync(
        List<Guid> courseIds)
    {   

        return await db.Courses
            .Where(c => courseIds.Contains(c.Id))
            .ToListAsync();
    }

    public async Task DeleteCourse(Course course)
    {
        db.Courses.Remove(course);
        await db.SaveChangesAsync();
    }


}