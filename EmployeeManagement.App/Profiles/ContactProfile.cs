using AutoMapper;
using EmployeeManagement.App.Dtos;
using EmployeeManagement.App.Models;

namespace EmployeeManagement.App.Profiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<ContactCreateDto, ContactRequest>();
        }
    }
}
