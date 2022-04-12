using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    public partial class Department
    {
        public Department()
        {
            Employee = new HashSet<Employee>();
        }

        [Key]
        public int IDDepartment { get; set; }
        [StringLength(500)]
        public string NameDepartment { get; set; } = null!;
        [StringLength(10)]
        public string? Owner { get; set; }
        public int? TenantID { get; set; }

        [InverseProperty("IDDepartmentNavigation")]
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
