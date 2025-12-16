using Dashboard.DAL.Models.Department;
using Dashboard.DAL.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Repositories.DepartmentRepo
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        //Spesific Signture
    }
}