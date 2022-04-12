using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    public partial class NuGetPackages
    {
        public long graph_id_92EDBB87F1FF4355967B916B3B7D2AF8 { get; set; }
        [Column("$node_id_B722C2BED99441A993735CD71FA3D85F")]
        [StringLength(1000)]
        public string _node_id_B722C2BED99441A993735CD71FA3D85F { get; set; } = null!;
        [Key]
        [StringLength(150)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
    }
}
