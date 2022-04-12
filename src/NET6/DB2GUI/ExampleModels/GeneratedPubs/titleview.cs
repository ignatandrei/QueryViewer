using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    [Keyless]
    public partial class titleview
    {
        [StringLength(80)]
        [Unicode(false)]
        public string title { get; set; } = null!;
        public byte? au_ord { get; set; }
        [StringLength(40)]
        [Unicode(false)]
        public string au_lname { get; set; } = null!;
        [Column(TypeName = "money")]
        public decimal? price { get; set; }
        public int? ytd_sales { get; set; }
        [StringLength(4)]
        [Unicode(false)]
        public string? pub_id { get; set; }
    }
}
