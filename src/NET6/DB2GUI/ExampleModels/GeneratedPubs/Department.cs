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
        public long IDDepartment { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        public int? TenantID { get; set; }

        [InverseProperty("IDDepartmentNavigation")]
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
