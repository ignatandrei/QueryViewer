using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    public partial class CustomerDemographics
    {
        public CustomerDemographics()
        {
            Customer = new HashSet<Customers>();
        }

        [Key]
        [StringLength(10)]
        public string CustomerTypeID { get; set; } = null!;
        [Column(TypeName = "ntext")]
        public string? CustomerDesc { get; set; }

        [ForeignKey("CustomerTypeID")]
        [InverseProperty("CustomerType")]
        public virtual ICollection<Customers> Customer { get; set; }
    }
}
