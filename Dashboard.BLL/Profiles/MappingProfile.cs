using AutoMapper;
using Dashboard.BLL.Dto_s.EmployeeDto_s;
using Dashboard.DAL.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dashboard.BLL.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDetailsDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<CreatedEmployeeDto, Employee>().ReverseMap();
            CreateMap<UpdatedEmployeeDto, Employee>().ReverseMap();

        }
    }
}
