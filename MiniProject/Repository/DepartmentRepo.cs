using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniProject.DTOs;
using MiniProject.Models;
using MiniProject.Models.DBContext;
using MiniProject.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniProject.Repository
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly MiniProjectDBContext _context;
        private readonly IMapper _mapper;

        public DepartmentRepo(MiniProjectDBContext miniProjectDBContext, IMapper mapper)
        {
            _context = miniProjectDBContext;
            _mapper = mapper;
        }
        public async Task<DepartmentDTO> AddDepartment(DepartmentDTO departmentDTO)
        {
            var Dep = _mapper.Map<Department>(departmentDTO);
            Dep.CreatedDate = DateTime.Now;
            Dep.NoOfEmp = 0;
            await _context.AddAsync(Dep);
            await _context.SaveChangesAsync();
            return departmentDTO;
        }

        public async Task<IEnumerable<Department>> ViewDepartments()
        {
            var res = await _context.Department.ToListAsync();
            return res;
        }
    }
}
