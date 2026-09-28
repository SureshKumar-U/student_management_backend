

using System.ComponentModel.DataAnnotations;
namespace CrudAPi.Dtos;

public class UpdateCourseRequestDto
{



    public string Name  {get;set;}= String.Empty;


    public string Code {get;set;} = String.Empty;
   
    public Guid  DepartmentId {get;set;}
}