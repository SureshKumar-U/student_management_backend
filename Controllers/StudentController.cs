using CrudAPi.Models;
using CrudAPi.Services;
using Microsoft.AspNetCore.Mvc;
using CrudAPi.Dtos;
using CrudAPi.common;
using Microsoft.AspNetCore.Authorization;
namespace CrudAPi.Controllers;

[ApiController]
[Route("api/v1/students")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;
    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    [Authorize(Roles = "STUDENT,ADMIN")]
    public async Task<IActionResult> GetAllStudents()
    {
        List<StudentResponseDto> students = await _studentService.GetAllStudents();
        return Ok(new ApiResponse<List<StudentResponseDto>>
        {
            message = "All studets fetched succesfully",
            status = 200,
            data = students,

        });
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "STUDENT,ADMIN")]

    public async Task<ActionResult<ApiResponse<Student>>> GetStudent(Guid id)
    {

        Student student = await _studentService.GetStudent(id);
        return Ok(new ApiResponse<Student>
        {
            message = "student fetched by id successfully",
            data = student,
            status = 200,
        });
    }

    [HttpGet("student/{id}")]
    [Authorize(Roles = "STUDENT,ADMIN")]

    public async Task<ActionResult<ApiResponse<Student>>> GetStudentById(Guid id)
    {

        Student student = await _studentService.GetStudentById(id);
        return Ok(new ApiResponse<Student>
        {
            message = "student fetched by id successfully",
            data = student,
            status = 200,
        });
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "STUDENT,ADMIN")]
    public async Task<IActionResult> UpdateStudent(Guid id, UpdateStudentDto updateStudentDto)
    {
        await _studentService.UpdateStudent(id, updateStudentDto);
        return Ok(new ApiResponse<string>
        {
            message = "Student updated succesfully",
            status = 200,
        });

    }
    [HttpDelete("{id}")]
    [Authorize(Roles = "STUDENT,ADMIN")]
    public async Task<IActionResult> DeleteStudent(Guid id)
    {
        await _studentService.DeleteStudent(id);
        return Ok(new ApiResponse<string>
        {
            message = "student deleted succesfully",
            status = 200,
            data = null,
        });

    }
    [Authorize(Roles = "STUDENT")]
    [HttpPost]
    public IActionResult CreateStudent(CreateStudentDto studentDto)
    {
        _studentService.AddStudent(studentDto);

        return Ok(new ApiResponse<string>
        {
            message = "student created succesfully",
            status = 201,
            data = null,
        });
    }

    [Authorize(Roles = "STUDENT")]
    [HttpGet("courses/{userId}")]
    public async Task<IActionResult> GetCoursesByUserId([FromRoute]  Guid userId)
    {
        var courses = await _studentService.GetCoursesByUserId(userId);

        return Ok(new
        {
            success = true,
            data = courses
        });
    }

    [Authorize(Roles = "ADMIN")]
    [HttpGet("recent")]
    public async Task<IActionResult> GetRecentStudents()
    {
        var courses = await _studentService.GetRecentStudents();

        return Ok(new
        {
            success = true,
            data = courses
        });
    }




}