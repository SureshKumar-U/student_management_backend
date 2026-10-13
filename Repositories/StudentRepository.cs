
using CrudAPi.Data;
using CrudAPi.Dtos;
using CrudAPi.Exceptions;
using CrudAPi.Models;
using Microsoft.EntityFrameworkCore;
namespace CrudAPi.Repositories;


public interface IStudentRepository
{
    Task CreateStudent(Student student);
    Task<bool> UserExistsByEmail(string email);
    Task<bool> DepartmentExists(Guid departmentId);
    Task<Student?> GetStudentDashboard(Guid userId);
    Task<StudentResponseDto?> GetStudentById(Guid userId);
    Task DeleteStudentById(Guid studentId);
    Task<List<StudentResponseDto>> GetAllStudents();
    Task UpdateStudent(Guid studentId, UpdateStudentDto UpdateStudent);
    Task<List<Course>> GetCoursesByUserId(Guid userId);
    Task<List<Student>> GetRecentStudents();
}

public class StudentRepository : IStudentRepository
{
    public AppDbContext db;
    public StudentRepository(AppDbContext dbContext)
    {
        db = dbContext;
    }
    public async Task CreateStudent(Student student)
    {
        await db.Students.AddAsync(student);
        await db.SaveChangesAsync();
    }
    public Task<bool> UserExistsByEmail(string email)
    {
        return db.Users.AnyAsync(user => user.Email == email);
    }
    public Task<bool> DepartmentExists(Guid departmentId)
    {
        return db.Departments.AnyAsync(department => department.Id == departmentId);
    }

    public async Task<StudentResponseDto?> GetStudentById(Guid studentId)
    {
        return await db.Students
                .Select(s => new StudentResponseDto
                {
                    Id = s.Id,
                    RollNumber = s.RollNumber,
                    Name = s.User.Name,
                    Email = s.User.Email,
                    Role = s.User.Role,
                    Department = s.Department == null
                ? null : new DepartmentResponseDto
                {
                    Id = s.Department.Id,
                    Name = s.Department.Name
                }
                }).FirstOrDefaultAsync(s => s.Id == studentId);
    }
    public async Task<Student?> GetStudentDashboard(Guid userId)
    {
        return await db.Students
        .Include(s => s.Department)
        .Include(s => s.User)
        .Include(s => s.Enrollments)
            .ThenInclude(e => e.Course)
        .FirstOrDefaultAsync(s => s.UserId == userId);
    }

    public async Task DeleteStudentById(Guid studentId)
    {
        Student? student = await db.Students.FindAsync(studentId);

        if (student == null)
        {
            throw new NotFoundException($"Student not found with ${studentId}");
        }
        if (student.User != null)
        {
            db.Users.Remove(student.User);
        }
        ;
        db.Students.Remove(student);
        await db.SaveChangesAsync();
    }


    public async Task<List<StudentResponseDto>> GetAllStudents()
    {

        return await db.Students
            .Select(s => new StudentResponseDto
            {
                Id = s.Id,
                RollNumber = s.RollNumber,
                Name = s.User.Name,
                Email = s.User.Email,
                Role = s.User.Role,
                Department = s.Department == null
                ? null : new DepartmentResponseDto
                {
                    Id = s.Department.Id,
                    Name = s.Department.Name
                }
            })
            .ToListAsync();
    }
    public async Task UpdateStudent(Guid id, UpdateStudentDto updateStudentDto)
    {
        Student student = await db.Students.Include(s => s.User)
        .Include(s => s.Department)
        .FirstOrDefaultAsync(s => s.Id == id);

        if (student == null)
        {
            throw new NotFoundException($"Student not found with {id}");
        }
        if (student.User == null)
        {
            throw new NotFoundException($"Student not foud with ${id}");
        }
        student.User.Email = updateStudentDto.Email ?? student.User.Email;
        student.User.Name = updateStudentDto.Name ?? student.User.Name;
        student.DepartmentId = updateStudentDto.DepartmentId ?? student.DepartmentId;
        db.Students.Update(student);
        await db.SaveChangesAsync();
    }

    public async Task<List<Course>> GetCoursesByUserId(Guid userId)
    {
        var student = await db.Students
            .FirstOrDefaultAsync(s => s.UserId == userId);
        if (student == null)
        {
            throw new NotFoundException($"Student not found for UserId: {userId}");
        }
        var courses = await db.Enrollments
            .Where(e => e.studentId == student.Id)
            .Include(e => e.Course)
                .ThenInclude(c => c.Department)
            .Select(e => e.Course)
            .ToListAsync();

        // var courses = await db.Enrollments
        //     .Where(e => e.studentId == student.Id)
        //     .Include(e => e.Course)
        //      .Include(e => e.Course)
        //     .Select(e => e.Course)
        //     .ToListAsync();

        return courses;
    }

    public async Task<List<Student>> GetRecentStudents()
    {

        var recentStudents = await db.Students
    .Include(s => s.User)
    .Include(s => s.Department)
    .OrderByDescending(s => s.CreatedAt)
    .Take(10)
    .ToListAsync();
        return recentStudents;
    }
}