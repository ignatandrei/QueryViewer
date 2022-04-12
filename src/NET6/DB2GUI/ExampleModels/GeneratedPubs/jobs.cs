using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    public partial class jobs
    {
        public jobs()
        {
            employee = new HashSet<employee>();
        }

        [Key]
        public short job_id { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string job_desc { get; set; } = null!;
        public byte min_lvl { get; set; }
        public byte max_lvl { get; set; }

        [InverseProperty("job")]
        public virtual ICollection<employee> employee { get; set; }
    }
}
