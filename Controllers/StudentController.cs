using CrudAPi.Models;
using CrudAPi.Services;
using Microsoft.AspNetCore.Mvc;
using CrudAPi.Dtos;
using CrudAPi.common;
using Microsoft.AspNetCore.Authorization;
namespace CrudAPi.Controllers;


[ApiController]
[Route("api/v1/students")]
public class StudentController: ControllerBase{
    private readonly IStudentService _studentService ; 
    public StudentController(IStudentService studentService){
       _studentService = studentService;
    }
    
    [HttpGet]
    [Authorize(Roles = "STUDENT,ADMIN")]
    public async Task<IActionResult> GetAllStudents(){
        List<StudentResponseDto> students = await _studentService.GetAllStudents();
        return Ok(new ApiResponse<List<StudentResponseDto>>
        {
            message="All studets fetched succesfully",
            status = 200,
            data = students,
            
        });
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Student")]
  
    public async Task<ActionResult<ApiResponse<Student>>> GetStudent(int id){
        
        Student student  = await  _studentService.GetStudent(id);
        return Ok(new ApiResponse<Student>
        {
            message= "student fetched by id successfully",
            data = student,
            status = 200,
        });
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> UpdateStudent(int id, UpdateStudentDto updateStudentDto){
        await _studentService.UpdateStudent(id,updateStudentDto);
        return Ok(new ApiResponse<string>
        {
            message="Student updated succesfully",
            status = 200,
        });

    }
    [HttpDelete("{id}")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> DeleteStudent(int id){
          await _studentService.DeleteStudent(id);
          return Ok(new ApiResponse<string>
          {
              message="student deleted succesfully",
              status= 200,
              data = null,
          });

    }
    [HttpPost()]
    public IActionResult CreateStudent(CreateStudentDto studentDto){
          _studentService.AddStudent(studentDto);

          return Ok(new ApiResponse<string>{
            message = "student created succesfully",
            status = 201,
            data=null,
            });
    }



}