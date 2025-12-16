using Dashboard.DAL.Contexts;
using Dashboard.DAL.Models.Department;
using Dashboard.DAL.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Repositories.DepartmentRepo
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

    }
}

