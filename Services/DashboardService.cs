namespace CrudAPi.Services;
using CrudAPi.Dtos;
using CrudAPi.Repositories;
public interface IAdminDashboardService
{
    Task<AdminDashboardStatsDto> GetDashboardStatsAsync();
}
public class AdminDashboardService : IAdminDashboardService
{
    private readonly IAdminDashboardRepository _repository;
    public AdminDashboardService(
        IAdminDashboardRepository repository)
    {
        _repository = repository;
    }

    public async Task<AdminDashboardStatsDto> GetDashboardStatsAsync()
    {
        return await _repository.GetDashboardStatsAsync();
    }
}

