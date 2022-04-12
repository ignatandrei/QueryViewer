using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    [Keyless]
    public partial class Relation_MyApp_EFCore
    {
        public long graph_id_C156D24305B54BA0A3FDB9BEEDB9D70E { get; set; }
        [Column("$edge_id_CCCE93F21AFA4A929F14969E14AE7D39")]
        [StringLength(1000)]
        public string _edge_id_CCCE93F21AFA4A929F14969E14AE7D39 { get; set; } = null!;
        public int from_obj_id_E08A53E12C59423FA768EBCB978BB9FD { get; set; }
        public long from_id_4A92EF1EBE3F4A3A86FD11CA4DAB9D43 { get; set; }
        [Column("$from_id_3CFA9C0908A0492DA8DD4411BE919004")]
        [StringLength(1000)]
        public string? _from_id_3CFA9C0908A0492DA8DD4411BE919004 { get; set; }
        public int to_obj_id_8497EBCF939A440DA411EF1680BDFE84 { get; set; }
        public long to_id_0F57A9118B394299B20A867749E465B5 { get; set; }
        [Column("$to_id_7485915F2AE44FE298CBB60BEF5AFE48")]
        [StringLength(1000)]
        public string? _to_id_7485915F2AE44FE298CBB60BEF5AFE48 { get; set; }
        public int? IDProject { get; set; }
    }
}
