
using CrudAPi.Data;
using CrudAPi.Dtos;
using CrudAPi.Exceptions;
using CrudAPi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudAPi.Repositories;


public interface IstudentRepository
{
    Task CreateStudent(Student student);
    Task<Student>? GetStudentById(int StudentId);
    Task DeleteStudentById(int studentId);
    Task<List<StudentResponseDto>> GetAllStudents();
    Task UpdateStudent(int studentId, UpdateStudentDto UpdateStudent);



}


public class StudentRepository : IstudentRepository
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

    public async Task<Student>? GetStudentById(int StudentId)
    {
        Student? student = await db.Students.FindAsync(StudentId);
        return student;
    }

    public async Task DeleteStudentById(int studentId)
    {
        Student? student = await db.Students.FindAsync(studentId);

        if (student == null)
        {
            throw new NotFoundException($"Student not found with ${studentId}");
        }
        ;
        db.Students.Remove(student);
        db.SaveChanges();
    }


    public async Task<List<StudentResponseDto>> GetAllStudents()
{
    return await db.Students
        .Select(s => new StudentResponseDto
        {
            Id = s.Id,
            RollNumber = s.RollNumber,

            User = new UserResponseDto
            {
                Id = s.User.Id,
                Name = s.User.Name,
                Email = s.User.Email,
                Role = s.User.Role
            },

            Department = new DepartmentResponseDto
            {
                Id = s.Department.Id,
                Name = s.Department.Name
            }
        })
        .ToListAsync();
}


    public async Task UpdateStudent(int id, UpdateStudentDto updateStudentDto)
    {
        Student student = await GetStudentById(id);

        if (student == null)
        {
            throw new NotFoundException($"Student not foud with ${id}");
        }
        student.User.Email = updateStudentDto.Email ?? student.User.Email;

        db.Students.Update(student);
        db.SaveChanges();

    }



}