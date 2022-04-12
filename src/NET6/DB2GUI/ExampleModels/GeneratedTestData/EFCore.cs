using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    public partial class EFCore
    {
        public long graph_id_51818B4A54934C2982999BAEECD4673E { get; set; }
        [Column("$node_id_991BC0372EB04458939D16F6EEA81361")]
        [StringLength(1000)]
        public string _node_id_991BC0372EB04458939D16F6EEA81361 { get; set; } = null!;
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
