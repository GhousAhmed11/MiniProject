using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniProject.DTOs;
using MiniProject.Models;
using MiniProject.Models.DBContext;
using MiniProject.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniProject.Repository
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly MiniProjectDBContext _context;
        private readonly IMapper _mapper;

        public EmployeeRepo(MiniProjectDBContext miniProjectDBContext, IMapper mapper)
        {
            _context = miniProjectDBContext;
            _mapper = mapper;
        }
        public async Task<EmployeeDTO> AddEmployee(EmployeeDTO employeeDTO)
        {
            var check = await _context.Department.FirstOrDefaultAsync(o => o.Id == employeeDTO.DepartmentId);
            if (check == null)
            {

                throw new ServiceException("No Department Exists");
            }
            check.NoOfEmp += 1;
            _context.Department.Update(check);
            var details = _mapper.Map<Employees>(employeeDTO);
            details.CreatedDate = DateTime.Now;
            await _context.AddAsync(details);
            await _context.SaveChangesAsync();
            return employeeDTO;

        }

        public async Task<Employees> ViewEmployee(int id)
        {
            var emp = await _context.Employees.FirstOrDefaultAsync(o => o.Id == id);
            return emp;
        }

        public async Task<IEnumerable<Employees>> ViewEmployees()
        {
            var allEmp = await _context.Employees.ToListAsync();
            return allEmp;
        }

        public async Task<EditEmployeeDTO> EditEmployee(EditEmployeeDTO editEmployeeDTO)
        {
            var check = await _context.Employees.FirstOrDefaultAsync(o => o.Id == editEmployeeDTO.Id);
            if (check == null)
            {

                throw new ServiceException("No Employee Exists");
            };
            check.Name = editEmployeeDTO.Name;
            check.Email = editEmployeeDTO.Email;
            check.Contact = editEmployeeDTO.Contact;
            _context.Employees.Update(check);
            await _context.SaveChangesAsync();
            return editEmployeeDTO;

        }
    }
}
