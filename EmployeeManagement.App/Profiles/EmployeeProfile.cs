using AutoMapper;
using EmployeeManagement.App.Dtos;
using EmployeeManagement.App.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeManagement.App.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeReadDto>();
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();
        }
    }
}

