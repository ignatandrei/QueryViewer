﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    [Keyless]
    public partial class Products_by_Category
    {
        [StringLength(15)]
        public string CategoryName { get; set; } = null!;
        [StringLength(40)]
        public string ProductName { get; set; } = null!;
        [StringLength(20)]
        public string? QuantityPerUnit { get; set; }
        public short? UnitsInStock { get; set; }
        public bool Discontinued { get; set; }
    }
}
