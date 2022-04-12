using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    [Table("Order Details")]
    [Index("OrderID", Name = "OrderID")]
    [Index("OrderID", Name = "OrdersOrder_Details")]
    [Index("ProductID", Name = "ProductID")]
    [Index("ProductID", Name = "ProductsOrder_Details")]
    public partial class Order_Details
    {
        [Key]
        public int OrderID { get; set; }
        [Key]
        public int ProductID { get; set; }
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

        [ForeignKey("OrderID")]
        [InverseProperty("Order_Details")]
        public virtual Orders Order { get; set; } = null!;
        [ForeignKey("ProductID")]
        [InverseProperty("Order_Details")]
        public virtual Products Product { get; set; } = null!;
    }
}
