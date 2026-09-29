



using CrudAPi.common;
using CrudAPi.Dtos;

using CrudAPi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;

[ApiController]
[Route("api/v1/courses")]
public class CourseController : ControllerBase
{

     public ICourseService courseService;
     public CourseController(ICourseService courseServc)
     {

          courseService = courseServc;
     }
     
     [HttpPost]
     [Authorize(Roles = "ADMIN,STUDENT")]
     public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequestDto createCourseDto)
     {
          await courseService.CreateCourse(createCourseDto);

          return StatusCode(StatusCodes.Status201Created,
              new ApiResponse<CreateCourseRequestDto>
              {
                   message = "Course create sucessfully",
                   status = 201,
                 
              }
          );
     }


     [HttpGet]
     [Authorize(Roles = "ADMIN,STUDENT")]

     public async Task<ActionResult<ApiResponse<List<CourseResponseDto>>>> GetAllCourses()
     {
          List<CourseResponseDto> courses = await courseService.GetAllCourses();
          ApiResponse<List<CourseResponseDto>> response = new ApiResponse<List<CourseResponseDto>>
          {
               message = "Course fetched succesfully",
               data = courses,

          };
          return Ok(response);
     }

     [HttpGet("{id}")]
     [Authorize(Roles = "ADMIN,STUDENT")]

     public async Task<ActionResult<ApiResponse<Course>>> GetCourseById(Guid id)
     {
          Course course = await courseService.GetCourseById(id);
          ApiResponse<Course> response = new ApiResponse<Course>
          {
               message = "Course fetched succesfully",
               data = course,

          };
          return Ok(response);
     }

     [HttpPut("{id}")]
     [Authorize(Roles = "ADMIN,STUDENT")]

     public async Task<IActionResult> UpdateCourseById([FromBody] UpdateCourseRequestDto courseDto, [FromRoute] Guid id)
     {
       
          await courseService.UpdateCourse(id, courseDto);
          ApiResponse<string> response = new ApiResponse<string>
          {
               message = "Course updted succesfully",
               status = 200
          };
          return Ok(response);
     }

     [HttpDelete("{id}")]
     [Authorize(Roles = "ADMIN,STUDENT")]

     public async Task<IActionResult> DeleteCourseById([FromRoute] Guid id)
     {
          await courseService.DeleteCourseById(id);
          ApiResponse<string> response = new ApiResponse<string>
          {
               message = "Course deleted succesfully",
               status = 200,
          };

          return Ok(response);
     }
     
}