namespace CrudAPi.Controllers;

using CrudAPi.common;
using CrudAPi.Dtos;
using CrudAPi.Services;
using CrudAPi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



[ApiController]
[Route("api/v1/student_enrollment")]
public class EnrollmentController : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService;

    public EnrollmentController(
        IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [HttpPost]
    public async Task<IActionResult> Enroll(
        [FromBody] EnrollStudentRequest request)
    {
        try
        {
            await _enrollmentService
                .EnrollStudentAsync(request);

            return Ok(new
            {
                message = "Student enrolled successfully."
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }
}
