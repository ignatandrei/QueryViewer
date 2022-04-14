using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    [Index("au_lname", "au_fname", Name = "aunmind")]
    public partial class authors
    {
        public authors()
        {
            titleauthor = new HashSet<titleauthor>();
        }

        [Key]
        [StringLength(11)]
        [Unicode(false)]
        public string au_id { get; set; } = null!;
        [StringLength(40)]
        [Unicode(false)]
        public string au_lname { get; set; } = null!;
        [StringLength(20)]
        [Unicode(false)]
        public string au_fname { get; set; } = null!;
        [StringLength(12)]
        [Unicode(false)]
        public string phone { get; set; } = null!;
        [StringLength(40)]
        [Unicode(false)]
        public string? address { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string? city { get; set; }
        [StringLength(2)]
        [Unicode(false)]
        public string? state { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string? zip { get; set; }
        public bool contract { get; set; }

        [InverseProperty("au")]
        public virtual ICollection<titleauthor> titleauthor { get; set; }
    }
}
