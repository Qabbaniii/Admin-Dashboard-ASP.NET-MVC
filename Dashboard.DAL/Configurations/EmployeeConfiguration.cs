using Dashboard.DAL.Models.Employees;
using Dashboard.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(d => d.Name).HasColumnType("varchar(50)");
            builder.Property(e => e.Gender)
                    .HasConversion(
                empGender => empGender.ToString(),
                 gender => (Gender)Enum.Parse(typeof(Gender), gender)
                 );
            builder.Property(e => e.EmployeeType)
                    .HasConversion(
                empType => empType.ToString(),
                 type => (EmployeeType)Enum.Parse(typeof(EmployeeType), type)
                 );
        }
    }
}