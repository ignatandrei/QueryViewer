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
        public int IDEmployee { get; set; }
        [StringLength(50)]
        public string NameEmployee { get; set; } = null!;
        public int IDDepartment { get; set; }

        [ForeignKey("IDDepartment")]
        [InverseProperty("Employee")]
        public virtual Department IDDepartmentNavigation { get; set; } = null!;
    }
}
