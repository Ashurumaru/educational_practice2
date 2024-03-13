using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edic_practice.model
{
    internal class CarMaintenence
    {
        public int MaintenanceID { get; set; }
        public string Model { get; set; }
        public DateTime MaintenanceStartDate { get; set; }
        public DateTime MaintenanceEndDate { get; set; }
        public string Description { get; set; }
        public virtual Car Car { get; set; }
    }
}
