using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Models.Shared
{
    public class BaseEntity
    {
        public int ID { get; set; }
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; }
    }
}
