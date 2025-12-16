using AutoMapper;
using Dashboard.BLL.Dto_s.EmployeeDto_s;
using Dashboard.DAL.Models.Employees;
using Dashboard.DAL.Repositories.EmployeeRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.BLL.Services.EmployeeService
{
    public class EmployeeServices : IEmployeeServices
    {

        private readonly IEmployeeRepository repository;
        private readonly IMapper mapper;

        public EmployeeServices(IEmployeeRepository Repository, IMapper mapper)
        {
            repository = Repository;
            this.mapper = mapper;
        }
        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var Employees = repository.GetAll();
            var MappedEmployees = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(Employees);
            return MappedEmployees;
        }

        public int CreateEmployee(CreatedEmployeeDto dto)
        {
            var Emp = mapper.Map<CreatedEmployeeDto, Employee>(dto);
            Emp.CreatedBy = 1;
            Emp.CreatedOn = DateTime.Now;
            Emp.LastModifiedBy = 1;
            Emp.LastModifiedOn = DateTime.Now;
            return repository.Add(Emp);
        }

        public int UpdateEmployee(UpdatedEmployeeDto dto)
        {
            var Emp = mapper.Map<UpdatedEmployeeDto, Employee>(dto);
            Emp.LastModifiedBy = 1;
            Emp.LastModifiedOn = DateTime.Now;
            return repository.Update(Emp);
        }

        public EmployeeDetailsDto GetEmployeeByID(int ID)
        {
            var employee = repository.GetByID(ID);
            var MappedEmployee = mapper.Map<Employee, EmployeeDetailsDto>(employee);
            return MappedEmployee;
        }
        public bool DeleteEmployee(int ID)
        {
            var dept = repository.GetByID(ID);
            if (dept is not null)
                return repository.Delete(ID) > 0;
            else
                return false;
        }

        public bool SoftDeleteEmployee(int ID)
        {
            var dept = repository.GetByID(ID);
            if (dept is not null)
                return repository.softDelete(ID) > 0;
            else
                return false;
        }


    }
}