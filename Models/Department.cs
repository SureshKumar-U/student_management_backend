namespace CrudAPi.Models;

using System.Text.Json.Serialization;

public class Department
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string Code { get; set; } = string.Empty;
  [JsonIgnore]
  public ICollection<Student> Students { get; set; }
      = new List<Student>();
  [JsonIgnore]
  public ICollection<Teacher> Teachers { get; set; }
    = new List<Teacher>();
  [JsonIgnore]
  public ICollection<Course> Courses { get; set; }
    = new List<Course>();
}
