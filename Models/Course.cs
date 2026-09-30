namespace CrudAPi.Models;
using System.Text.Json.Serialization;

public class Course
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Guid DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    [JsonIgnore]
    public ICollection<Enrollment> Enrollments { get; set; }
        = new List<Enrollment>();
    public ICollection<Teacher> Teachers { get; set; }
        = new List<Teacher>();
}
