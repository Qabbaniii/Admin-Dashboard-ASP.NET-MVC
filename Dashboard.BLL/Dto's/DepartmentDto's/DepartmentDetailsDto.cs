using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.BLL.Dto_s.DepartmentDto_s
{
    public class DepartmentDetailsDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public int CreatedBy { get; set; }
        public DateOnly CreatedOn { get; set; }
        public int LastModifiedBy { get; set; }
        public DateOnly LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }

    }
}
