using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    [Keyless]
    public partial class vwDepEmp
    {
        public int IDDepartment { get; set; }
        public int IDEmployee { get; set; }
    }
}
