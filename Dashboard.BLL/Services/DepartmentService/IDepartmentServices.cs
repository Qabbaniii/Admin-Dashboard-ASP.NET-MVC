using Dashboard.BLL.Dto_s.DepartmentDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.BLL.Services.DepartmentService
{
    public interface IDepartmentServices
    {
        public IEnumerable<DepartmentDto> GetAllDepartments();
        public DepartmentDetailsDto GetDepartmentByID(int ID);
        public int AddDepartment(CreatedDepartmentDto dto);
        public int UpdateDepartment(UpdatedDepartmentDto dto);
        public bool DeleteDepartment(int ID);
        public bool SoftDeleteDepartment(int ID);

    }
}
