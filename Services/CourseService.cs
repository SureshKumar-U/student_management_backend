namespace CrudAPi.Models;
using CrudAPi.Dtos;
using CrudAPi.Exceptions;
using CrudAPi.Models;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;



public interface ICourseService
{
    Task CreateCourse(CreateCourseRequestDto createCourseDto);
    Task<Course> GetCourseById(Guid id);
    Task DeleteCourseById(Guid courseId);
    Task UpdateCourse(Guid id, UpdateCourseRequestDto updateCourseDto);
    Task<List<CourseResponseDto>> GetAllCourses();



}
public class CourseService : ICourseService
{

    public ICourseRepository courseRepository;
    public IDepartmentRepository departmentRepository;

    public CourseService(ICourseRepository courseRepo, IDepartmentRepository departmentRepo)
    {
        courseRepository = courseRepo;
        departmentRepository = departmentRepo;
    }

    public async Task CreateCourse(CreateCourseRequestDto createCourseDto)
    {
        Course course = new Course
        {
            Name = createCourseDto.Name ?? "",
            DepartmentId = createCourseDto.DepartmentId,
            Code = createCourseDto.Code ?? ""

        };
        await courseRepository.CreateCourse(course);
    }

    public async Task<Course> GetCourseById(Guid id)
    {
        Course course = await courseRepository.GetCourseById(id);
        if (course == null)
        {
            throw new NotFoundException($"Course Not Found with id ${id}");
        }
        return course;
    }

    public async Task DeleteCourseById(Guid courseId)
    {
        Course course = await courseRepository.GetCourseById(courseId);
        if (course == null)
        {
            throw new NotFoundException($"Course Not Found with found ${courseId}");
        }
        await courseRepository.DeleteCourse(course);
    }

    public async Task UpdateCourse(Guid id, UpdateCourseRequestDto updateCourseDto)
    {
        if (updateCourseDto == null)
        {
            throw new BadHttpRequestException("Course update data is required");
        }

        Course? course = await courseRepository.GetCourseById(id);
        Department? department = await departmentRepository.GetDepartmentById(updateCourseDto.DepartmentId);


        if (course == null)
        {
            throw new NotFoundException($"Course bot Found with id: ${id}");
        }
        ;
        if (department == null)
        {
            throw new NotFoundException($"Department Not Found with id: ${id}");
        }
        ;

        course.Name = updateCourseDto.Name ?? course.Name;
        course.Code = updateCourseDto.Code ?? course.Code;
        course.DepartmentId = updateCourseDto.DepartmentId;
        await courseRepository.UpdateCourse(course);
    }

    public async Task<List<CourseResponseDto>> GetAllCourses()
    {
        return await courseRepository.GetAllCourses();

    }
}