using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    [Index("title", Name = "titleind")]
    public partial class titles
    {
        public titles()
        {
            sales = new HashSet<sales>();
            titleauthor = new HashSet<titleauthor>();
        }

        [Key]
        [StringLength(6)]
        [Unicode(false)]
        public string title_id { get; set; } = null!;
        [StringLength(80)]
        [Unicode(false)]
        public string title { get; set; } = null!;
        [StringLength(12)]
        [Unicode(false)]
        public string type { get; set; } = null!;
        [StringLength(4)]
        [Unicode(false)]
        public string? pub_id { get; set; }
        [Column(TypeName = "money")]
        public decimal? price { get; set; }
        [Column(TypeName = "money")]
        public decimal? advance { get; set; }
        public int? royalty { get; set; }
        public int? ytd_sales { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string? notes { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime pubdate { get; set; }

        [ForeignKey("pub_id")]
        [InverseProperty("titles")]
        public virtual publishers? pub { get; set; }
        [InverseProperty("title")]
        public virtual ICollection<sales> sales { get; set; }
        [InverseProperty("title")]
        public virtual ICollection<titleauthor> titleauthor { get; set; }
    }
}
