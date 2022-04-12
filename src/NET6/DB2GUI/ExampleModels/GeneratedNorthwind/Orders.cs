using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    [Index("CustomerID", Name = "CustomerID")]
    [Index("CustomerID", Name = "CustomersOrders")]
    [Index("EmployeeID", Name = "EmployeeID")]
    [Index("EmployeeID", Name = "EmployeesOrders")]
    [Index("OrderDate", Name = "OrderDate")]
    [Index("ShipPostalCode", Name = "ShipPostalCode")]
    [Index("ShippedDate", Name = "ShippedDate")]
    [Index("ShipVia", Name = "ShippersOrders")]
    public partial class Orders
    {
        public Orders()
        {
            Order_Details = new HashSet<Order_Details>();
        }

        [Key]
        public int OrderID { get; set; }
        [StringLength(5)]
        public string? CustomerID { get; set; }
        public int? EmployeeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? OrderDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RequiredDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        [Column(TypeName = "money")]
        public decimal? Freight { get; set; }
        [StringLength(40)]
        public string? ShipName { get; set; }
        [StringLength(60)]
        public string? ShipAddress { get; set; }
        [StringLength(15)]
        public string? ShipCity { get; set; }
        [StringLength(15)]
        public string? ShipRegion { get; set; }
        [StringLength(10)]
        public string? ShipPostalCode { get; set; }
        [StringLength(15)]
        public string? ShipCountry { get; set; }

        [ForeignKey("CustomerID")]
        [InverseProperty("Orders")]
        public virtual Customers? Customer { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("Orders")]
        public virtual Employees? Employee { get; set; }
        [ForeignKey("ShipVia")]
        [InverseProperty("Orders")]
        public virtual Shippers? ShipViaNavigation { get; set; }
        [InverseProperty("Order")]
        public virtual ICollection<Order_Details> Order_Details { get; set; }
    }
}
