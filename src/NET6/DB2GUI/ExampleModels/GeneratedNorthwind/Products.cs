using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    [Index("CategoryID", Name = "CategoriesProducts")]
    [Index("CategoryID", Name = "CategoryID")]
    [Index("ProductName", Name = "ProductName")]
    [Index("SupplierID", Name = "SupplierID")]
    [Index("SupplierID", Name = "SuppliersProducts")]
    public partial class Products
    {
        public Products()
        {
            Order_Details = new HashSet<Order_Details>();
        }

        [Key]
        public int ProductID { get; set; }
        [StringLength(40)]
        public string ProductName { get; set; } = null!;
        public int? SupplierID { get; set; }
        public int? CategoryID { get; set; }
        [StringLength(20)]
        public string? QuantityPerUnit { get; set; }
        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        [ForeignKey("CategoryID")]
        [InverseProperty("Products")]
        public virtual Categories? Category { get; set; }
        [ForeignKey("SupplierID")]
        [InverseProperty("Products")]
        public virtual Suppliers? Supplier { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<Order_Details> Order_Details { get; set; }
    }
}
