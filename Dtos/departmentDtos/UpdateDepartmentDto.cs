namespace CrudAPi.Dtos;

using System.ComponentModel.DataAnnotations;

public class UpdateDepartmentRequestDto
{
   
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "Department name must be between 2 and 100 characters.")]
    public string Name { get; set; } = string.Empty;


    [StringLength(10,
        ErrorMessage = "Department code cannot exceed 10 characters.")]
    [RegularExpression(@"^[A-Z]+$",
        ErrorMessage = "Department code must contain only uppercase letters.")]
    public string Code { get; set; } = string.Empty; // CS, ECE, CE

}