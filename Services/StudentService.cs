namespace CrudAPi.Services;  
using CrudAPi.Models;    //import models
using CrudAPi.Dtos;
using CrudAPi.Repositories;
using CrudAPi.Exceptions;
using StudentManagement.Models;

public interface IStudentService
{
    Task<List<StudentResponseDto>> GetAllStudents();
    Task<Student> GetStudent(Guid id);
    Task<Student> GetStudentById(Guid id);
    Task AddStudent(CreateStudentDto studentDto);
    Task UpdateStudent(Guid id, UpdateStudentDto updateStudentDto);
    Task DeleteStudent(Guid id);
    Task<List<Course>> GetCoursesByUserId(Guid userId);
    Task<List<Student>> GetRecentStudents();
};



public class StudentService : IStudentService
{
    public IstudentRepository studentRepository;
    public StudentService(IstudentRepository studentRepo)
    {
        studentRepository = studentRepo;
    }
    public async Task<Student> GetStudent(Guid id)
    {
        Student? student = await studentRepository.GetStudentByUserId(id);
        if (student == null)
        {
            throw new NotFoundException($"User with ID {id} does not exist.");
        }
        return student;
    }
    public async Task<Student> GetStudentById(Guid id)
    {
        Student student = await studentRepository.GetStudentById(id);
        if (student == null)
        {
            throw new NotFoundException($"User with ID {id} does not exist.");
        }
        return student;
    }
    public async Task<List<StudentResponseDto>> GetAllStudents()
    {
        return await studentRepository.GetAllStudents();
    }
    public async Task UpdateStudent(Guid id, UpdateStudentDto updateStudentDto)
    {
        await studentRepository.UpdateStudent(id, updateStudentDto);
    }
    public async Task DeleteStudent(Guid id)
    {
        await studentRepository.DeleteStudentById(id);
    }
    public async Task AddStudent(CreateStudentDto studentDto)
    {
        Student student = new Student
        {
            UserId = studentDto.UserId,
            DepartmentId = studentDto.DepartmentId,
            RollNumber = studentDto.RollNumber
        };
        await studentRepository.CreateStudent(student);
    }

    public async Task<List<Course>> GetCoursesByUserId(Guid userId)
    {
        return await studentRepository.GetCoursesByUserId(userId);
    }
    public async Task<List<Student>> GetRecentStudents()
    {
        return await studentRepository.GetRecentStudents();
    }
}