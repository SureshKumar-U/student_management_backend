namespace CrudAPi.Controllers;

using CrudAPi.common;
using CrudAPi.Dtos;
using CrudAPi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;

[ApiController]
[Route("api/v1/departments")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService departmentService;
    public DepartmentController(IDepartmentService deptMentService)
    {
        departmentService = deptMentService;
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> CreateDepartment(CreateDepartmentRequestDto createDepartmentDto)
    {
        await departmentService.CreateDepartment(createDepartmentDto);
        ApiResponse<string> response = new ApiResponse<string>
        {
            message = "Department created successfully",
            status = 201,
        };
        return StatusCode(StatusCodes.Status201Created, response);
    }

     [HttpPut("{id}")]
     [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> UpdateDepartment([FromRoute] Guid id, [FromBody] UpdateDepartmentRequestDto updateDepartmentRequestDto)
    {
        await departmentService.UpdateDepartmentById(id, updateDepartmentRequestDto);
        ApiResponse<string> response = new ApiResponse<string>
        {
            message = "Department Updated successfully",
            status = 200,
        };
        return Ok(response);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<ApiResponse<Department>>> GetDepartmentById([FromRoute] Guid id)
    {
        Department? department = await departmentService.GetDepartment(id);
        ApiResponse<Department> response = new ApiResponse<Department>
        {
            message = "Department Updated successfully",
            status = 200,
            data = department,
        };
        return Ok(response);
    }

    [HttpGet]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<ApiResponse<List<Department>>>> GetAllDepartments()
    {
        List<Department> departments = await departmentService.GetAllDepartments();
       ApiResponse<List<Department>> response = new ApiResponse<List<Department>>
        {
            message = "Department Updated successfully",
            status = 200,
            data = departments,
        };
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> DeleteDepartment([FromRoute] Guid id)
    {
        await departmentService.DeleteDepartment(id);
       ApiResponse<object> response = new ApiResponse<object>
        {
            message = "Department Deleted successfully",
            status = 200,
      
        };
        return Ok(response);
    }


}