using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    public partial class Territories
    {
        public Territories()
        {
            Employee = new HashSet<Employees>();
        }

        [Key]
        [StringLength(20)]
        public string TerritoryID { get; set; } = null!;
        [StringLength(50)]
        public string TerritoryDescription { get; set; } = null!;
        public int RegionID { get; set; }

        [ForeignKey("RegionID")]
        [InverseProperty("Territories")]
        public virtual Region Region { get; set; } = null!;

        [ForeignKey("TerritoryID")]
        [InverseProperty("Territory")]
        public virtual ICollection<Employees> Employee { get; set; }
    }
}
