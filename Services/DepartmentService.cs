
using CrudAPi.Dtos;
using CrudAPi.Exceptions;
using CrudAPi.Models;
using StudentManagement.Models;

public interface IDepartmentService
{

    Task<List<Department>> GetAllDepartments();
    Task<Department> GetDepartment(Guid departmentId);

    Task UpdateDepartmentById(Guid departmentId, UpdateDepartmentRequestDto updateDepartmentDto);

    Task CreateDepartment(CreateDepartmentRequestDto createDepartmentDto);
    Task DeleteDepartment(Guid departmentId);
    
}
public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository departmentRepository;
    public DepartmentService(IDepartmentRepository departmentRepo)
    {
        departmentRepository = departmentRepo;
    }

    public async Task CreateDepartment(CreateDepartmentRequestDto createDepartmentDto)  
     {
        Department existingDepartment = await  departmentRepository.GetDepartmentByName(createDepartmentDto.Name);
        if(existingDepartment != null)
        {
            throw new Exception("department Already existed");
        }
        Department department = new Department
        {
            Name = createDepartmentDto.Name,
        };
       await departmentRepository.CreateDepartment(department);  
     } 

     public async Task UpdateDepartmentById(Guid id, UpdateDepartmentRequestDto updateDepartmentDto)
    {
        Department? department = await departmentRepository.GetDepartmentById(id);
        if(department == null)
        {
            throw new NotFoundException($"Department Not Found with id: ${id}");
        }
            department.Name = updateDepartmentDto.Name ?? department.Name;
            department.Code = updateDepartmentDto.Code ?? department.Code;
            await departmentRepository.UpdateDepartment(department);
    }

    public  async Task<List<Department>> GetAllDepartments()
    {
      return  await departmentRepository.GetAllDepartments();
        
    }

    public async Task<Department> GetDepartment(Guid id)
    {
       Department? department = await departmentRepository.GetDepartmentById(id);
       if(department == null) throw new NotFoundException($"Departement Not Found with id ${id}");
       
       return department;
    }

        public async Task DeleteDepartment(Guid id)
    {
       Department? department = await departmentRepository.GetDepartmentById(id);
       if(department == null) throw new NotFoundException($"Departement Not Found with id ${id}");
       
       await departmentRepository.DeleteDepartment(department);
    }





}