using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edic_practice.model
{
    internal class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
        public virtual ICollection<UserReview> UserReviews { get; set; }
    }
}
