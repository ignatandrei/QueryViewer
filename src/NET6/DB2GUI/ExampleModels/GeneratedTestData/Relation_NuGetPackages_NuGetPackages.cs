using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    [Keyless]
    public partial class Relation_NuGetPackages_NuGetPackages
    {
        public long graph_id_5AEA23E55E4B4AB49730EF6B77925B29 { get; set; }
        [Column("$edge_id_99A6A869D3C34D15B7007346721A94D3")]
        [StringLength(1000)]
        public string _edge_id_99A6A869D3C34D15B7007346721A94D3 { get; set; } = null!;
        public int from_obj_id_3A0A7B04CD884A289807DB009F72AFFB { get; set; }
        public long from_id_BA500AC98E5A4CE49162E43F7601AC1F { get; set; }
        [Column("$from_id_6ED5A99C63D84AAB938D3DCC519FEBC7")]
        [StringLength(1000)]
        public string? _from_id_6ED5A99C63D84AAB938D3DCC519FEBC7 { get; set; }
        public int to_obj_id_D987536304FB48AFBAEADA533AE37E7B { get; set; }
        public long to_id_8B20F1322BB24D60B8DD79E6804DDC96 { get; set; }
        [Column("$to_id_03655BEA2E114CDD833C2B8E61EAD6DA")]
        [StringLength(1000)]
        public string? _to_id_03655BEA2E114CDD833C2B8E61EAD6DA { get; set; }
        [StringLength(30)]
        public string IDVersionPackage { get; set; } = null!;
    }
}
