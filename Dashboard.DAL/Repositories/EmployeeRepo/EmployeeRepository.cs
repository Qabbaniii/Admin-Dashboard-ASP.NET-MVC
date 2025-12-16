using Dashboard.DAL.Contexts;
using Dashboard.DAL.Models.Employees;
using Dashboard.DAL.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Repositories.EmployeeRepo
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
