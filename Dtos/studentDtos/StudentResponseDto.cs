namespace CrudAPi.Dtos;

public class StudentResponseDto
{
    public Guid? Id { get; set; }
    public Guid? UserId { get; set; }
    public string? RollNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public DepartmentResponseDto? Department { get; set; }
}

public class DepartmentResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
