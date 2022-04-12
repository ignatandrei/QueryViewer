using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    public partial class sales
    {
        [Key]
        [StringLength(4)]
        [Unicode(false)]
        public string stor_id { get; set; } = null!;
        [Key]
        [StringLength(20)]
        [Unicode(false)]
        public string ord_num { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime ord_date { get; set; }
        public short qty { get; set; }
        [StringLength(12)]
        [Unicode(false)]
        public string payterms { get; set; } = null!;
        [Key]
        [StringLength(6)]
        [Unicode(false)]
        public string title_id { get; set; } = null!;

        [ForeignKey("stor_id")]
        [InverseProperty("sales")]
        public virtual stores stor { get; set; } = null!;
        [ForeignKey("title_id")]
        [InverseProperty("sales")]
        public virtual titles title { get; set; } = null!;
    }
}
