using CrudAPi.Data;
using CrudAPi.Dtos;
using Microsoft.EntityFrameworkCore;

namespace CrudAPi.Repositories;




    public interface IAdminDashboardRepository
    {
        Task<AdminDashboardStatsDto> GetDashboardStatsAsync();
    }






    public class AdminDashboardRepository : IAdminDashboardRepository
    {
        private readonly AppDbContext _context;

        public AdminDashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AdminDashboardStatsDto> GetDashboardStatsAsync()
        {
            var studentsCount = await _context.Students.CountAsync();
            var coursesCount = await _context.Courses.CountAsync();
            var departmentsCount = await _context.Departments.CountAsync();

            return new AdminDashboardStatsDto
            {
                Students = studentsCount,
                Courses = coursesCount,
                Departments = departmentsCount
            };
        }
    }

