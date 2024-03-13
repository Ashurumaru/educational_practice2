using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edic_practice.model
{
    internal class CarType
    {
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}
