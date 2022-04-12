using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    public partial class publishers
    {
        public publishers()
        {
            employee = new HashSet<employee>();
            titles = new HashSet<titles>();
        }

        [Key]
        [StringLength(4)]
        [Unicode(false)]
        public string pub_id { get; set; } = null!;
        [StringLength(40)]
        [Unicode(false)]
        public string? pub_name { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string? city { get; set; }
        [StringLength(2)]
        [Unicode(false)]
        public string? state { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string? country { get; set; }

        [InverseProperty("pub")]
        public virtual pub_info pub_info { get; set; } = null!;
        [InverseProperty("pub")]
        public virtual ICollection<employee> employee { get; set; }
        [InverseProperty("pub")]
        public virtual ICollection<titles> titles { get; set; }
    }
}
