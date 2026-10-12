namespace CrudAPi.Dtos;
using System.ComponentModel.DataAnnotations;

public class CreateStudentDto
{
    [Required]
    [StringLength(50)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Password { get; set; } = string.Empty;

    [Required]
    public Guid? DepartmentId { get; set; }

    [Required]
    public string RollNumber { get; set; } = string.Empty;
}
