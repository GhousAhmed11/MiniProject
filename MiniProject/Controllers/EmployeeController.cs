using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject.DTOs;
using MiniProject.Models;
using MiniProject.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepo _EmployeeRepo;

        public EmployeeController( IEmployeeRepo employeeRepo)
        {
            _EmployeeRepo = employeeRepo;
        }
        [ApiAuthorize.ApiAuthorize(UserRole.Admin)]
        [HttpPost("employee")]
        public async Task<EmployeeDTO> AddEmployee(EmployeeDTO Employee)
        {
            var res = await _EmployeeRepo.AddEmployee(Employee);
            return res;
        }
        [ApiAuthorize.ApiAuthorize(UserRole.Admin)]
        [HttpGet("employees")]
        public async Task<IEnumerable<Employees>> ViewEmployees()
        {
            var res = await _EmployeeRepo.ViewEmployees();
            return res;
        }
        [ApiAuthorize.ApiAuthorize]
        [HttpGet("employee-detail")]
        public async Task<Employees> ViewEmployee(int id)
        {
            var res = await _EmployeeRepo.ViewEmployee(id);
            return res;
        }

        [ApiAuthorize.ApiAuthorize(UserRole.Admin)]
        [HttpPost("edit-employee")]
        public async Task<EditEmployeeDTO> EditEmployee(EditEmployeeDTO editEmployeeDTO)
        {
            var res = await _EmployeeRepo.EditEmployee(editEmployeeDTO);
            return res;
        }
    }
}
