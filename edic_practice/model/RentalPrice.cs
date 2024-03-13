using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edic_practice.model
{
    internal class RentalPrice
    {
        public int PriceID { get; set; }
        public int CarID { get; set; }
        public decimal DailyPrice { get; set; }
        public virtual Car Car { get; set; }
    }
}
