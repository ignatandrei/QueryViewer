using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    public partial class Employee
    {
        [Key]
        public long IDEmployee { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        public long IDDepartment { get; set; }

        [ForeignKey("IDDepartment")]
        [InverseProperty("Employee")]
        public virtual Department IDDepartmentNavigation { get; set; } = null!;
    }
}
