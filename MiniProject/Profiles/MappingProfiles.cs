using AutoMapper;
using MiniProject.DTOs;
using MiniProject.Models;

namespace MiniProject.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AppUser, AppUserDTO>().ReverseMap();
            CreateMap<Department, DepartmentDTO>().ReverseMap();
            CreateMap<Employees, EmployeeDTO>().ReverseMap();
        }
    }
}
