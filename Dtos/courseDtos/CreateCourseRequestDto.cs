

using System.ComponentModel.DataAnnotations;
namespace CrudAPi.Dtos;
public class CreateCourseRequestDto
{
    [Required]
    [StringLength(50)]
    public string Name {get;set;} = String.Empty;

    [StringLength(100)]
    public string Code {get;set;} = String.Empty;

    public Guid  DepartmentId {get;set;}
}