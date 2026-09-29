

using System.Text.Json.Serialization;

namespace CrudAPi.Models;


public class Course
{
    public Guid Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public Guid DepartmentId { get; set; }

    public Department Department { get; set; } = null!;

    // Students enrolled in this course
    // Students enrolled through Enrollment
    [JsonIgnore]
    public ICollection<Enrollment> Enrollments { get; set; }
        = new List<Enrollment>();

    // Teachers teaching this course
    public ICollection<Teacher> Teachers { get; set; }
        = new List<Teacher>();
}
