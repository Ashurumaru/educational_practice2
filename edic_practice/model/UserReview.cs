using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edic_practice.model
{
    internal class UserReview
    {
        public int ReviewID { get; set; }
        public string Username { get; set; }
        public DateTime ReviewDate { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public virtual User User { get; set; }
    }
}
