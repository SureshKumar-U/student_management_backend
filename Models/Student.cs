using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudentManagement.Models;
namespace CrudAPi.Models;

public class Student
{ 
    [Key] 
    public Guid Id { get; set; }

    public Guid DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    public Guid? UserId { get; set; }
    public User? User { get; set; }

    public string RollNumber { get; set; }

    // Courses
    public ICollection<Course> Courses { get; set; }
        = new List<Course>();
}