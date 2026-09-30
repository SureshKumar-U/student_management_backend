namespace CrudAPi.Services;

using CrudAPi.Dtos;
using CrudAPi.Exceptions;
using CrudAPi.Models;
using CrudAPi.Repositories;
public interface IEnrollmentService
{
    Task EnrollStudentAsync(
        EnrollStudentRequest request);
}


public class EnrollmentService : IEnrollmentService
{
    private readonly IstudentRepository _studentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;
    public EnrollmentService(
        IstudentRepository studentRepository,
        ICourseRepository courseRepository,
        IEnrollmentRepository enrollmentRepository)
    {
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _enrollmentRepository = enrollmentRepository;
    }
    public async Task EnrollStudentAsync(
        EnrollStudentRequest request)
    {
        Console.WriteLine(request.CourseId);
        Console.WriteLine(request.UserId);
        // Check student
        var student = await _studentRepository
            .GetStudentByUserId(request.UserId);
        if (student == null)
            throw new Exception("Student not found.");
        // Remove duplicate course IDs
        var courseId = request.CourseId;
        var course = await _courseRepository.GetCourseById(courseId);
        if (course == null)
            throw new NotFoundException(
                "Course  was not found.");
        var alreadyEnrolled =
            await _enrollmentRepository.ExistsAsync(
                student.Id,
                course.Id);
        if (alreadyEnrolled)
            throw new Exception(
               "Student is already enrolled in the selected courses.");
        var enrollment = new Enrollment
        {
            studentId = student.Id,
            courseId = course.Id,
            EnrolledDate = DateTime.UtcNow,
            Status = EnrollmentStatus.Enrolled
        };
        await _enrollmentRepository
            .AddRangeAsync(enrollment);
        await _enrollmentRepository.SaveChangesAsync();
    }
}
