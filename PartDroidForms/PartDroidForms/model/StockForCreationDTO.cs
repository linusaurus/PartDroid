using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartDroidForms.Model
{
    public class StockForCreationDTO
    {
        public int PartID { get; set; }
        public decimal Quantity { get; set; }
        public int TransType { get; set; }
        public string User { get; set; }
        public int EmployeeID { get; set; }
        public int Unit { get; set; }
        public DateTime DateStamp { get; set; }
    }
}
