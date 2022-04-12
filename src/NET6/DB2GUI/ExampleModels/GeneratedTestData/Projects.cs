using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    [Keyless]
    public partial class Projects
    {
        public long graph_id_01EE4EF844BE4506BEE61087B86BA7AE { get; set; }
        [Column("$node_id_5AB4D5AA10544ECE9B91A539A0F52DAB")]
        [StringLength(1000)]
        public string _node_id_5AB4D5AA10544ECE9B91A539A0F52DAB { get; set; } = null!;
        [StringLength(150)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [StringLength(500)]
        [Unicode(false)]
        public string? Description { get; set; }
    }
}
