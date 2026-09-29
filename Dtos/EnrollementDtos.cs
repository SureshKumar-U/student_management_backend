namespace CrudAPi.Dtos;


public class EnrollStudentRequest
{
    public Guid UserId { get; set; }

    public Guid  CourseId { get; set; } = new();
}



