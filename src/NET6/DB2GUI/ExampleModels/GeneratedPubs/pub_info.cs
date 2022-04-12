using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    public partial class pub_info
    {
        [Key]
        [StringLength(4)]
        [Unicode(false)]
        public string pub_id { get; set; } = null!;
        [Column(TypeName = "image")]
        public byte[]? logo { get; set; }
        [Column(TypeName = "text")]
        public string? pr_info { get; set; }

        [ForeignKey("pub_id")]
        [InverseProperty("pub_info")]
        public virtual publishers pub { get; set; } = null!;
    }
}
