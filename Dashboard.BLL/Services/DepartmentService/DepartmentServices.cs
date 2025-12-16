using Dashboard.BLL.Dto_s.DepartmentDto_s;
using Dashboard.BLL.Factory.DepartmentFactory;
using Dashboard.DAL.Repositories.DepartmentRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.BLL.Services.DepartmentService
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IDepartmentRepository _repository;
        public DepartmentServices(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var AllDepartments = _repository.GetAll();

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
            var department = _repository.GetByID(ID);
            if (department is null) return null;
            else
            {

                return department.ToDepartmentDetailsDto();
            }

        }
        public int AddDepartment(CreatedDepartmentDto dto)
        {
            var dept = dto.FromCreatedDepartmentToDepartment();

            return _repository.Add(dept);

        }
        public int UpdateDepartment(UpdatedDepartmentDto dto)
        {
            var dept = dto.FromUpdatedDepartmentToDepartment();
            return _repository.Update(dept);
        }

        public bool DeleteDepartment(int ID)
        {
            var dept = _repository.GetByID(ID);
            if (dept is not null)
                return _repository.Delete(ID) > 0;
            else
                return false;
        }

        public bool SoftDeleteDepartment(int ID)
        {
            var dept = _repository.GetByID(ID);
            if (dept is not null)
                return _repository.softDelete(ID) > 0;
            else
                return false;
        }
    }
}
