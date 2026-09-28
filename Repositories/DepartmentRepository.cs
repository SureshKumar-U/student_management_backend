



using CrudAPi.Data;
using CrudAPi.Dtos;
using CrudAPi.Models;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;



public interface IDepartmentRepository
{
    Task<List<Department>> GetAllDepartments();
    Task<Department?> GetDepartmentById(Guid departmentId);
    Task CreateDepartment(Department department);
    Task UpdateDepartment(Department department);
    Task DeleteDepartment(Department department);


}


public class DeparmentRepository:IDepartmentRepository
{
     public readonly AppDbContext db;
    
    public DeparmentRepository(AppDbContext dbContext)
    { 
        db = dbContext;
    }
     
    public async Task<List<Department>> GetAllDepartments()
    {
         return await db.Departments.ToListAsync();
    }


    public async Task<Department?> GetDepartmentById(Guid departmentId)
    {   Console.WriteLine(departmentId);
        return await db.Departments.FindAsync(departmentId);
    }

    public async Task CreateDepartment(Department department)
    {
           await db.Departments.AddAsync(department);
           await db.SaveChangesAsync();
    }

    public async Task UpdateDepartment(Department department)
    {
          db.Departments.Update(department);
          await db.SaveChangesAsync();
    }

    public async Task DeleteDepartment(Department department)
    {
          db.Departments.Remove(department);
          await db.SaveChangesAsync();
    }
    






    



}