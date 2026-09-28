
using System.ComponentModel.DataAnnotations;

namespace CrudAPi.Dtos;

public class SignUpUserDto
{

    [Required]
    [StringLength(50)]
    public string Email { get; set; } = string.Empty;


    [Required]
    public string Name { get; set; } = string.Empty;

    [StringLength(100)]
    public string Password { get; set; } = string.Empty;

    public UserRole Role { get; set; }



}