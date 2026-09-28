namespace CrudAPi.Dtos;
public class StudentResponseDto
{
    public Guid Id { get; set; }
    public string RollNumber { get; set; } = string.Empty;

    public UserResponseDto User { get; set; } = null!;
    public DepartmentResponseDto Department { get; set; } = null!;
}



public class DepartmentResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
