
using Dashboard.BLL.Dto_s.DepartmentDto_s;
using Dashboard.BLL.Factory.DepartmentFactory;
using Dashboard.DAL.Repositories.DepartmentRepo;
using Dashboard.DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.BLL.Services.DepartmentService
{
    public class DepartmentServices : IDepartmentServices
    {
        
        private readonly IUnitOfWork unitOfWork;

        public DepartmentServices(IUnitOfWork unitOfWork)
        {
            
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var AllDepartments = unitOfWork.departmentRepository.GetAll();

            List<DepartmentDto> mappedDepartments = new List<DepartmentDto>();
            foreach (var department in AllDepartments)
            {
                var mappedDepart = department.ToDepartmentDto();
                mappedDepartments.Add(mappedDepart);
            }
            return mappedDepartments;
        }
        public DepartmentDetailsDto GetDepartmentByID(int ID)
        {
            var department = unitOfWork.departmentRepository.GetByID(ID);
            if (department is null) return null;
            else
            {

                return department.ToDepartmentDetailsDto();
            }

        }
        public int AddDepartment(CreatedDepartmentDto dto)
        {
            var dept = dto.FromCreatedDepartmentToDepartment();

            unitOfWork.departmentRepository.Add(dept);
            return unitOfWork.Complete();

        }
        public int UpdateDepartment(UpdatedDepartmentDto dto)
        {
            var dept = dto.FromUpdatedDepartmentToDepartment();
            unitOfWork.departmentRepository.Update(dept);
            return unitOfWork.Complete();
        }

        public bool DeleteDepartment(int ID)
        {
            var dept = unitOfWork.departmentRepository.GetByID(ID);
            if (dept is not null)
            {
                unitOfWork.employeeRepository.Delete(ID);
                return unitOfWork.Complete() > 0;
            }
            else
                return false;
        }

        public bool SoftDeleteDepartment(int ID)
        {
            var dept = unitOfWork.departmentRepository.GetByID(ID);
            if (dept is not null)
            {
                unitOfWork.employeeRepository.softDelete(ID);
                return unitOfWork.Complete() > 0;
            }
            else
                return false;
        }
    }
}
