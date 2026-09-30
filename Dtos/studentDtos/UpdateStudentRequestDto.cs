namespace CrudAPi.Dtos;
using System.ComponentModel.DataAnnotations;

public class UpdateStudentDto{
    [StringLength(50)]
    public string Name { get; set; }

     public string Email { get; set; }
    public Guid? DepartmentId { get; set; }

}