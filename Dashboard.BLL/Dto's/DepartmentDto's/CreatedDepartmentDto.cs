using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.BLL.Dto_s.DepartmentDto_s
{
    public class CreatedDepartmentDto
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Code is Required")]
        public string Code { get; set; }
    }
}
