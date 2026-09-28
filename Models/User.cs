using CrudAPi.Models;

namespace StudentManagement.Models;

public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public Student? Student { get; set; }

    public Teacher? Teacher { get; set; }
}

