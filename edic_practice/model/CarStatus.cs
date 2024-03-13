using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edic_practice.model
{
    internal class CarStatus
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}
