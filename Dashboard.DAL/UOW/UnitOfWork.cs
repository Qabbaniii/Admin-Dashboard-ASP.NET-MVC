using Dashboard.DAL.Contexts;
using Dashboard.DAL.Repositories.DepartmentRepo;
using Dashboard.DAL.Repositories.EmployeeRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public UnitOfWork(ApplicationDbContext context)
        {
             employeeRepository = new EmployeeRepository(context);
            departmentRepository = new DepartmentRepository(context);
            this.context = context;
        }
        public IEmployeeRepository employeeRepository { get; set; }

        public IDepartmentRepository departmentRepository { get; set; }

        public int Complete()
        {
           return context.SaveChanges();
        }
    }
}
