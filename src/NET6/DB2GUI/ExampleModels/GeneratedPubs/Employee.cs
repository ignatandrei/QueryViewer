using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    public partial class employee
    {
        [Key]
        [StringLength(9)]
        [Unicode(false)]
        public string emp_id { get; set; } = null!;
        [StringLength(20)]
        [Unicode(false)]
        public string fname { get; set; } = null!;
        [StringLength(1)]
        [Unicode(false)]
        public string? minit { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string lname { get; set; } = null!;
        public short job_id { get; set; }
        public byte? job_lvl { get; set; }
        [StringLength(4)]
        [Unicode(false)]
        public string pub_id { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime hire_date { get; set; }

        [ForeignKey("job_id")]
        [InverseProperty("employee")]
        public virtual jobs job { get; set; } = null!;
        [ForeignKey("pub_id")]
        [InverseProperty("employee")]
        public virtual publishers pub { get; set; } = null!;
    }
}
