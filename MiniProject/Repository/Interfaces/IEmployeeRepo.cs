using MiniProject.DTOs;
using MiniProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniProject.Repository.Interfaces
{
    public interface IEmployeeRepo
    {
        Task<EmployeeDTO> AddEmployee(EmployeeDTO employeeDTO);
        Task<IEnumerable<Employees>> ViewEmployees();
        Task<Employees> ViewEmployee(int id);
        Task<EditEmployeeDTO> EditEmployee(EditEmployeeDTO editEmployeeDTO);
        Task<EmployeeDTO> AddEmployeeWithExtra(EmployeeDTO employeeDTO);
        Task<EmployeeDTO> ViewEmployeeWithExtra(int id);

    }
}
