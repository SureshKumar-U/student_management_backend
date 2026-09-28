using System.ComponentModel.DataAnnotations;
namespace CrudAPi.Dtos;

public class CreateDepartmentRequestDto
{
    [Required(ErrorMessage = "Department name is required.")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "Department name must be between 2 and 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Department code is required.")]
    [StringLength(10,
        ErrorMessage = "Department code cannot exceed 10 characters.")]
    [RegularExpression(@"^[A-Z]+$",
        ErrorMessage = "Department code must contain only uppercase letters.")]
    public string Code { get; set; } = string.Empty; // CS, ECE, CE

    [StringLength(500,
        ErrorMessage = "Description cannot exceed 500 characters.")]
    public string? Description { get; set; }

    public bool Active { get; set; } = true;
}