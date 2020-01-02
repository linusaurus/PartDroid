using System;
using System.Collections.Generic;
using System.Text;

namespace PartDroidForms.Model
{
    public class Part
    {
        public int PartID { get; set; }
        public string description { get; set; }
        public string partNumber { get; set; }
        public decimal? Cost { get; set; }
        public string Location { get; set; }       
        public string SKU { get; set; }
        public decimal? StockOnHand { get; set; }

    }
}
