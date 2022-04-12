using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    public partial class stores
    {
        public stores()
        {
            sales = new HashSet<sales>();
        }

        [Key]
        [StringLength(4)]
        [Unicode(false)]
        public string stor_id { get; set; } = null!;
        [StringLength(40)]
        [Unicode(false)]
        public string? stor_name { get; set; }
        [StringLength(40)]
        [Unicode(false)]
        public string? stor_address { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string? city { get; set; }
        [StringLength(2)]
        [Unicode(false)]
        public string? state { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string? zip { get; set; }

        [InverseProperty("stor")]
        public virtual ICollection<sales> sales { get; set; }
    }
}
