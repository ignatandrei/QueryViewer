using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    [Keyless]
    public partial class Relation_Projects_NuGetPackages
    {
        public long graph_id_E22B10D44EDF4C6C842060BC02236BC0 { get; set; }
        [Column("$edge_id_B9EF8609156148C785C734D6D7B59A29")]
        [StringLength(1000)]
        public string _edge_id_B9EF8609156148C785C734D6D7B59A29 { get; set; } = null!;
        public int from_obj_id_633F720B8E2F43A498CF5509D78D0EC2 { get; set; }
        public long from_id_F99818A215EC43A3921893E0FC2E8459 { get; set; }
        [Column("$from_id_943D77DE7E7A4627AC87CAD0256FB797")]
        [StringLength(1000)]
        public string? _from_id_943D77DE7E7A4627AC87CAD0256FB797 { get; set; }
        public int to_obj_id_3416FA4992B4404AAB7A1898AD95B40F { get; set; }
        public long to_id_54935D0297994284912B2C5D5A840D24 { get; set; }
        [Column("$to_id_FBC7D853D3F240A89D9C889D2DB0A6E8")]
        [StringLength(1000)]
        public string? _to_id_FBC7D853D3F240A89D9C889D2DB0A6E8 { get; set; }
        public int IDProject { get; set; }
        [StringLength(30)]
        public string IDVersionPackage { get; set; } = null!;
    }
}
