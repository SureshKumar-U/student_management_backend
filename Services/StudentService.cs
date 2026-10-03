namespace CrudAPi.Services;  
using CrudAPi.Models;    //import models
using CrudAPi.Dtos;
using CrudAPi.Repositories;
using CrudAPi.Exceptions;

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
    private readonly IStudentRepository  _studentRepository;
    public StudentService(IStudentRepository  studentRepo)
    {
        _studentRepository = studentRepo;
    }
    public async Task<Student> GetStudent(Guid id)
    {
        Student? student = await _studentRepository.GetStudentByUserId(id);
        if (student == null)
        {
            throw new NotFoundException($"User with ID {id} does not exist.");
        }
        return student;
    }
    public async Task<Student> GetStudentById(Guid id)
    {
        Student student = await _studentRepository.GetStudentById(id);
        if (student == null)
        {
            throw new NotFoundException($"User with ID {id} does not exist.");
        }
        return student;
    }
    public async Task<List<StudentResponseDto>> GetAllStudents()
    {
        return await _studentRepository.GetAllStudents();
    }
    public async Task UpdateStudent(Guid id, UpdateStudentDto updateStudentDto)
    {
        await _studentRepository.UpdateStudent(id, updateStudentDto);
    }
    public async Task DeleteStudent(Guid id)
    {
        await _studentRepository.DeleteStudentById(id);
    }
    public async Task AddStudent(CreateStudentDto studentDto)
    {
        Student student = new Student
        {
            UserId = studentDto.UserId,
            DepartmentId = studentDto.DepartmentId,
            RollNumber = studentDto.RollNumber
        };
        await _studentRepository.CreateStudent(student);
    }

    public async Task<List<Course>> GetCoursesByUserId(Guid userId)
    {
        return await _studentRepository.GetCoursesByUserId(userId);
    }
    public async Task<List<Student>> GetRecentStudents()
    {
        return await _studentRepository.GetRecentStudents();
    }
}