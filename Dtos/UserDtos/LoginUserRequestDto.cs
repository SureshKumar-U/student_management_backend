
using System.ComponentModel.DataAnnotations;

public class LoginUserDto{

    [Required]
    [StringLength(50)]
    public string Email { get; set; } = string.Empty;
    

    [StringLength(100)]
    public string Password { get; set; } = string.Empty;
    

}