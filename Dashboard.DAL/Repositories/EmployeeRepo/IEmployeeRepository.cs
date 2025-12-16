using Dashboard.DAL.Models.Employees;
using Dashboard.DAL.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Repositories.EmployeeRepo
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        //Spesific Signture
    }
}
