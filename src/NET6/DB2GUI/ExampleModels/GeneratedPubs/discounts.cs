using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    [Keyless]
    public partial class discounts
    {
        [StringLength(40)]
        [Unicode(false)]
        public string discounttype { get; set; } = null!;
        [StringLength(4)]
        [Unicode(false)]
        public string? stor_id { get; set; }
        public short? lowqty { get; set; }
        public short? highqty { get; set; }
        [Column(TypeName = "decimal(4, 2)")]
        public decimal discount { get; set; }

        [ForeignKey("stor_id")]
        public virtual stores? stor { get; set; }
    }
}
