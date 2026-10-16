using CrudAPi.Data;
using CrudAPi.Dtos;
using CrudAPi.Exceptions;
using CrudAPi.Models;
using Microsoft.AspNetCore.Mvc;
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
    Task<List<StudentResponseDto>> GetAllStudents(int pageNo, int pageSize);
    Task UpdateStudent(Guid userId, UpdateStudentDto updateStudentDto);
    Task<List<Course>> GetCoursesByUserId(Guid userId);
    Task<List<StudentResponseDto>> GetRecentStudents();
    Task<bool> HasStudentsInDepartment(Guid departmentId);
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

    public async Task<StudentResponseDto?> GetStudentById(Guid userId)
    {
        return await db.Users
                .Where(u => u.Id == userId && u.Role == UserRole.STUDENT)
                .Select(u => new StudentResponseDto
                {
                    Id = u.Student == null ? null : u.Student.Id,
                    UserId = u.Id,
                    RollNumber = u.Student == null ? null : u.Student.RollNumber,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role,
                    Department = u.Student == null || u.Student.Department == null
                        ? null
                        : new DepartmentResponseDto
                        {
                            Id = u.Student.Department.Id,
                            Name = u.Student.Department.Name
                        }
                }).FirstOrDefaultAsync();
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
    public async Task<List<StudentResponseDto>> GetAllStudents(int pageNo, int pageSize)
    {
        return await db.Users
            .Where(u => u.Role == UserRole.STUDENT)
            .OrderByDescending(u => u.CreatedAt)
            .ThenBy(u => u.Id)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .Select(u => new StudentResponseDto
            {
                UserId = u.Id,
                RollNumber = u.Student == null ? null : u.Student.RollNumber,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role,
                Department = u.Student == null || u.Student.Department == null
                    ? null
                    : new DepartmentResponseDto
                    {
                        Id = u.Student.Department.Id,
                        Name = u.Student.Department.Name
                    }
            })
            .ToListAsync();
    }
    public async Task UpdateStudent(Guid userId, UpdateStudentDto updateStudentDto)
    {
        User? user = await db.Users
            .Include(u => u.Student)
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            throw new NotFoundException($"User not found with ID {userId}");
        }
        if (user.Role != UserRole.STUDENT)
        {
            throw new NotFoundException($"Student user not found with ID {userId}");
        }

        if (updateStudentDto.DepartmentId.HasValue &&
            !await db.Departments.AnyAsync(d => d.Id == updateStudentDto.DepartmentId.Value))
        {
            throw new NotFoundException(
                $"Department not found with ID {updateStudentDto.DepartmentId.Value}");
        }
        user.Name = updateStudentDto.Name ?? user.Name;
        user.Email = updateStudentDto.Email ?? user.Email;
        Student student = user.Student ?? new Student
        {
            UserId = user.Id,
            User = user
        };
        student.DepartmentId =
            updateStudentDto.DepartmentId ?? student.DepartmentId;

        if (user.Student == null)
        {
            await db.Students.AddAsync(student);
        }
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
        return courses;
    }

    public async Task<List<StudentResponseDto>> GetRecentStudents()
    {
        var recentStudents = await db.Users
        .Where(u => u.Role == UserRole.STUDENT)
    .OrderByDescending(s => s.CreatedAt)
    .Take(5)
    .Select(u => new StudentResponseDto
    {
        UserId = u.Id,
        Id = u.Student.Id,
        Name = u.Name,
        Email = u.Email,
        Role = u.Role,
        RollNumber = u.Student == null ? null : u.Student.RollNumber,
        Department = u.Student == null || u.Student.Department == null
            ? null
            : new DepartmentResponseDto
            {
                Id = u.Student.Department.Id,
                Name = u.Student.Department.Name
            }
    })
    .ToListAsync();
        return recentStudents;
    }

    public Task<bool> HasStudentsInDepartment(Guid departmentId)
    {
        return db.Students.AnyAsync(s => s.DepartmentId == departmentId);
    }
}