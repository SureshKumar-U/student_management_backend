using CrudAPi.common;
using Microsoft.AspNetCore.Mvc;
using CrudAPi.Services;
using Microsoft.AspNetCore.Authorization;
namespace CrudAPi.Controllers;

[ApiController]
[Route("api/v1/admin/dashboard")]
public class AdminDashboardController : ControllerBase
{
    private readonly IAdminDashboardService _service;
    public AdminDashboardController(
        IAdminDashboardService service)
    {
        _service = service;
    }
    [HttpGet("stats")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> GetDashboardStats()
    {
        var result = await _service.GetDashboardStatsAsync();
        ApiResponse<object> response
         = new ApiResponse<object>
         {
             message = "Dashboard details  successfully",
             status = 200,
             data = result,
         };
        return StatusCode(StatusCodes.Status200OK, response);
    }
}

