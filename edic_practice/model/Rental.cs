using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edic_practice.model
{
    internal class Rental
    {
        public int RentalID { get; set; }
        public string Model { get; set; }
        public string Username { get; set; }
        public DateTime RentalStartDate { get; set; }
        public DateTime RentalEndDate { get; set; }
        public bool IsReturned { get; set; }
        public virtual Car Car { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<RentalPayment> RentalPayments { get; set; }
    }
}
