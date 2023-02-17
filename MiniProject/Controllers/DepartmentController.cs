using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject.ApiAuthorize;
using MiniProject.DTOs;
using MiniProject.Models;
using MiniProject.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepo _departmentRepo;

        public DepartmentController(IDepartmentRepo departmentRepo )
        {
            _departmentRepo = departmentRepo;

        }
        [ApiAuthorize.ApiAuthorize(UserRole.Admin)]
        [HttpPost("department")]
        public async Task<DepartmentDTO> AddDepartment(DepartmentDTO department)
        {
            var res = await _departmentRepo.AddDepartment(department);
            return res;
        }
        [ApiAuthorize.ApiAuthorize(UserRole.Admin)]
        [HttpGet("departments")]
        public async Task<IEnumerable<Department>> Viewepartments()
        {
            var res = await _departmentRepo.ViewDepartments();
            return res;
        }
    }
}
