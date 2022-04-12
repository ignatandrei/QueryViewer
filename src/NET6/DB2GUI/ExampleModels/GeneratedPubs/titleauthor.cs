using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    public partial class titleauthor
    {
        [Key]
        [StringLength(11)]
        [Unicode(false)]
        public string au_id { get; set; } = null!;
        [Key]
        [StringLength(6)]
        [Unicode(false)]
        public string title_id { get; set; } = null!;
        public byte? au_ord { get; set; }
        public int? royaltyper { get; set; }

        [ForeignKey("au_id")]
        [InverseProperty("titleauthor")]
        public virtual authors au { get; set; } = null!;
        [ForeignKey("title_id")]
        [InverseProperty("titleauthor")]
        public virtual titles title { get; set; } = null!;
    }
}
