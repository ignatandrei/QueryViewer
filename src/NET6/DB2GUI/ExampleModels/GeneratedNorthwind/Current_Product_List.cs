using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    [Keyless]
    public partial class Current_Product_List
    {
        public int ProductID { get; set; }
        [StringLength(40)]
        public string ProductName { get; set; } = null!;
    }
}
