using CrudAPi.Models;

namespace StudentManagement.Models;

public class Department
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

     public string Code { get; set; } = string.Empty;


    public ICollection<Student> Students { get; set; }
        = new List<Student>();

    public ICollection<Teacher> Teachers { get; set; }
        = new List<Teacher>();

    public ICollection<Course> Courses { get; set; }
        = new List<Course>();
}
