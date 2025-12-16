using Dashboard.BLL.Dto_s.DepartmentDto_s;
using Dashboard.DAL.Models.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.BLL.Factory.DepartmentFactory
{
    public static class DepartmentFact
    {
        public static DepartmentDto ToDepartmentDto(this Department D)
        {
            return new DepartmentDto
            {
                Id = D.ID,
                Name = D.Name,
                Code = D.Code,
                Description = D.Description,
            };
        }
        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto
            {
                ID = department.ID,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreatedOn = DateOnly.FromDateTime(department.CreatedOn),
                CreatedBy = department.CreatedBy,
                LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedOn),
                LastModifiedBy = department.LastModifiedBy,

            };
        }

        public static Department FromCreatedDepartmentToDepartment(this CreatedDepartmentDto createdDepartment)
        {
            return new Department
            {

                Name = createdDepartment.Name,
                Code = createdDepartment.Code,
                Description = createdDepartment.Description,
                CreatedBy = 1,
                CreatedOn = DateTime.Now,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now



            };
        }
        public static Department FromUpdatedDepartmentToDepartment(this UpdatedDepartmentDto updatedDepartment)
        {
            return new Department
            {
                ID = updatedDepartment.ID,
                Name = updatedDepartment.Name,
                Code = updatedDepartment.Code,
                Description = updatedDepartment.Description,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
                IsDeleted = false
            };

        }
    }
}
