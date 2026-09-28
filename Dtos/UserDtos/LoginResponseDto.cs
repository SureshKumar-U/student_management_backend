using StudentManagement.Models;

namespace CrudAPi.Dtos;

public class LoginResponseDto
{
    public string Token { get; set; }
    public Guid Id { get; set; }

    public String Email { get; set; }
    public String Name { get; set; }

    public UserRole Role { get; set; }
}