using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edic_practice.model
{
    internal class Car
    {
        public int CarID { get; set; }
        public string TypeName { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int CarYears { get; set; }
        public string LicensePlate { get; set; }
        public string StatusName { get; set; }
        public virtual CarStatus CarStatus { get; set; }
        public virtual CarType CarType { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
        public virtual ICollection<CarMaintenance> CarMaintenances { get; set; }
    }
}
