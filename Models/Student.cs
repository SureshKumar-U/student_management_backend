using System.ComponentModel.DataAnnotations;
namespace CrudAPi.Models;


public class Student
{
    [Key]
    public Guid Id { get; set; }
    public Guid? DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
    public Guid? UserId { get; set; }
    public User? User { get; set; }
    public string? RollNumber { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Enrollment> Enrollments { get; set; }
        = new List<Enrollment>();
}