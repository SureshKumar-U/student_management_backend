using CrudAPi.Models;    //import models
using CrudAPi.Dtos;
using CrudAPi.Repositories;
using CrudAPi.Exceptions;
using StudentManagement.Models;

namespace CrudAPi.Services;  // import  services

public interface IStudentService
{
  Task<List<StudentResponseDto>> GetAllStudents();
  Task<Student> GetStudent(int id);
  Task AddStudent(CreateStudentDto studentDto);
  Task UpdateStudent(int id,UpdateStudentDto updateStudentDto);
  Task DeleteStudent(int id);
}



public class StudentService : IStudentService{
    // public List<Student> students;

    public IstudentRepository studentRepository;

    public StudentService(IstudentRepository studentRepo)
    {
         studentRepository = studentRepo;
    }

    public async Task<Student> GetStudent(int id){
        Student student = await studentRepository.GetStudentById(id);
        if(student == null)
        {
            
            throw new NotFoundException($"User with ID {id} does not exist.");
        }
        return student;

    }

    public async Task<List<StudentResponseDto>> GetAllStudents(){
        return await studentRepository.GetAllStudents();
    }

    public async Task UpdateStudent(int id, UpdateStudentDto updateStudentDto){
         await studentRepository.UpdateStudent(id,updateStudentDto);

    }
    
    public async Task DeleteStudent(int id){
        await  studentRepository.DeleteStudentById(id);
    } 
     public async Task AddStudent(CreateStudentDto studentDto){

      Student student = new Student
      {
          UserId= studentDto.UserId,
          DepartmentId = studentDto.DepartmentId,
          RollNumber = studentDto.RollNumber
      };
      await studentRepository.CreateStudent(student);
    
    } 

}