using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    [Keyless]
    public partial class Category_Sales_for_1997
    {
        [StringLength(15)]
        public string CategoryName { get; set; } = null!;
        [Column(TypeName = "money")]
        public decimal? CategorySales { get; set; }
    }
}
