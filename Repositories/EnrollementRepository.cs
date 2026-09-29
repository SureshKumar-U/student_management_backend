

using CrudAPi.Data;
using CrudAPi.Models;
using Microsoft.EntityFrameworkCore;
namespace CrudAPi.Repositories;

public interface IEnrollmentRepository
{
    Task<bool> ExistsAsync(
        Guid studentId,
        Guid courseId);

    Task AddRangeAsync(
        Enrollment enrollment);

    Task SaveChangesAsync();
}


public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly AppDbContext _context;

    public EnrollmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(
        Guid studentId,
        Guid courseId)


    {

        var exists = await _context.Enrollments
           .AnyAsync(e =>
               e.studentId == studentId &&
               e.courseId == courseId);
        Console.WriteLine($"Exists: {exists}");
        return exists;
    }

    public async Task AddRangeAsync(
        Enrollment enrollment)
    {
        await _context.Enrollments
            .AddRangeAsync(enrollment);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
