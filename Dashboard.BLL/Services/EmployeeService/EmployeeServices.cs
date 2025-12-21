using AutoMapper;
using Dashboard.BLL.Common.Services.Attachment;
using Dashboard.BLL.Dto_s.EmployeeDto_s;
using Dashboard.DAL.Models.Employees;
using Dashboard.DAL.Repositories.EmployeeRepo;
using Dashboard.DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.BLL.Services.EmployeeService
{
    public class EmployeeServices : IEmployeeServices
    {

        
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IAttachmentServices attachmentServices;

        public EmployeeServices(IUnitOfWork unitOfWork, IMapper mapper,IAttachmentServices attachmentServices)
        {
            
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.attachmentServices = attachmentServices;
        }
        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var Employees = unitOfWork.employeeRepository.GetAll();
            var MappedEmployees = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(Employees);
            return MappedEmployees;
        }
        public IEnumerable<EmployeeDto> GetSearchedEmployees(string? searchvalue)
        {
            var Employees = unitOfWork.employeeRepository.GetAll(searchvalue);
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

            if (dto.Image is not null)
            {
              Emp.ImageName = attachmentServices.UploadImage(dto.Image, "images");
            }
            unitOfWork.employeeRepository.Add(Emp);
            return unitOfWork.Complete();
        }

        public int UpdateEmployee(UpdatedEmployeeDto dto)
        {
            var Emp = mapper.Map<UpdatedEmployeeDto, Employee>(dto);
            Emp.LastModifiedBy = 1;
            Emp.LastModifiedOn = DateTime.Now;

            if (dto.Image is not null)
            {
                if(Emp.ImageName is not null)
                {
                    var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "images", Emp.ImageName);
                    attachmentServices.DeleteImage(filepath);
                }
                Emp.ImageName = attachmentServices.UploadImage(dto.Image, "images");
            }
            unitOfWork.employeeRepository.Update(Emp);
            return unitOfWork.Complete();
        }

        public EmployeeDetailsDto GetEmployeeByID(int ID)
        {
            var employee = unitOfWork.employeeRepository.GetByID(ID);
            var MappedEmployee = mapper.Map<Employee, EmployeeDetailsDto>(employee);
            return MappedEmployee;
        }
        public bool DeleteEmployee(int ID)
        {
            var emp = unitOfWork.employeeRepository.GetByID(ID);
            if (emp is not null)
            { 
                if(emp.ImageName is not null)
                {
                    var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "images",emp.ImageName);
                    attachmentServices.DeleteImage(filepath);
                }
                unitOfWork.employeeRepository.Delete(ID);
                return unitOfWork.Complete() > 0;
            }
            else
                return false;
        }

        public bool SoftDeleteEmployee(int ID)
        {
            var emp = unitOfWork.employeeRepository.GetByID(ID);
            if (emp is not null)
            {
                unitOfWork.employeeRepository.softDelete(ID);
                return unitOfWork.Complete() > 0;
            }
            else
                return false;
        }


    }
}