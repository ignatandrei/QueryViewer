using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    [Keyless]
    public partial class roysched
    {
        [StringLength(6)]
        [Unicode(false)]
        public string title_id { get; set; } = null!;
        public int? lorange { get; set; }
        public int? hirange { get; set; }
        public int? royalty { get; set; }

        [ForeignKey("title_id")]
        public virtual titles title { get; set; } = null!;
    }
}
