using Dashboard.BLL.Dto_s.EmployeeDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.BLL.Services.EmployeeService
{
    public interface IEmployeeServices
    {
        public IEnumerable<EmployeeDto> GetAllEmployees();
        public EmployeeDetailsDto GetEmployeeByID(int ID);
        public int CreateEmployee(CreatedEmployeeDto dto);
        public int UpdateEmployee(UpdatedEmployeeDto dto);
        public bool DeleteEmployee(int ID);
        public bool SoftDeleteEmployee(int ID);
    }
}
