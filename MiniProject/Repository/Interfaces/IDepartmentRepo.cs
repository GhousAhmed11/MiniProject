using MiniProject.DTOs;
using MiniProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniProject.Repository.Interfaces
{
    public interface IDepartmentRepo
    {
        Task<DepartmentDTO> AddDepartment(DepartmentDTO departmentDTO);
        Task<IEnumerable<Department>> ViewDepartments();
    }
}
