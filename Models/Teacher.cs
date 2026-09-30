namespace CrudAPi.Models;

public class Teacher
{
    public Guid Id { get; set; }
    public string Phone { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
    // Courses taught
    public ICollection<Course> Courses { get; set; }
        = new List<Course>();
}
