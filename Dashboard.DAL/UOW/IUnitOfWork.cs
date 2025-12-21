using Dashboard.DAL.Repositories.DepartmentRepo;
using Dashboard.DAL.Repositories.EmployeeRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.UOW
{
    public interface IUnitOfWork
    {
        public IEmployeeRepository employeeRepository { get; }
        public IDepartmentRepository departmentRepository { get; }
        public int Complete();
    }
}
