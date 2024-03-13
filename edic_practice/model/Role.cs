﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edic_practice.model
{
    internal class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
}
