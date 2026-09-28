using CrudAPi.Models;

namespace StudentManagement.Models;

public class Course
{
    public Guid Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public Guid DepartmentId { get; set; }

    public Department Department { get; set; } = null!;

    // Students enrolled in this course
    public ICollection<Student> Students { get; set; }
        = new List<Student>();

    // Teachers teaching this course
    public ICollection<Teacher> Teachers { get; set; }
        = new List<Teacher>();
}
