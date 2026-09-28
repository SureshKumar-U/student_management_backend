namespace CrudAPi.Dtos;
using System.ComponentModel.DataAnnotations;

public class CreateStudentDto
{
    public Guid UserId { get; set; }
    public Guid DepartmentId { get; set; }
    public string RollNumber { get; set; } = null!;
}
