using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    public partial class MyApp
    {
        public long graph_id_97F25F5C3BB64666809F7401D4144968 { get; set; }
        [Column("$node_id_1AEFF6D54F0B470DBA5C36F066B16939")]
        [StringLength(1000)]
        public string _node_id_1AEFF6D54F0B470DBA5C36F066B16939 { get; set; } = null!;
        [Key]
        [StringLength(50)]
        public string Version { get; set; } = null!;
        [StringLength(150)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [StringLength(500)]
        [Unicode(false)]
        public string? Description { get; set; }
    }
}
