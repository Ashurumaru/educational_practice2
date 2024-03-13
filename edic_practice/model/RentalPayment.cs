using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edic_practice.model
{
    internal class RentalPayment
    {
        public int PaymentID { get; set; }
        public int RentalID { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public virtual Rental Rental { get; set; }

    }
}
