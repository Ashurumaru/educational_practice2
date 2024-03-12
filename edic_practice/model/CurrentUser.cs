using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edic_practice.model
{
    internal class CurrentUser
    {
        public static string Username { get; set; }
        public static int Role { get; set; }
        public static string FirstName { get; set; }
        public static string SecondName { get; set; }
        public static string Patronymic { get; set; }

        public static string GetFullName()
        {
            return $"{FirstName} {SecondName} {Patronymic}";
        }
    }
}
