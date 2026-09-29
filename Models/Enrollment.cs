

using System.Text.Json.Serialization;
using CrudAPi.Models;



public class Enrollment
{
    public Guid id {get;set;}

    public Guid studentId {get;set;}
    
    [JsonIgnore]
    public Student Student {get;set;} = null!;

    public Guid courseId {get;set;}
    public Course Course {get;set;}
   
    public DateTime EnrolledDate {get;set;}

    public EnrollmentStatus Status {get;set;} = EnrollmentStatus.Enrolled;


}