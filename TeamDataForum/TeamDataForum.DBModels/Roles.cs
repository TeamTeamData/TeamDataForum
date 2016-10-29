﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamDataForum.DBModels
{
    public class Roles
    {
        private ISet<User> user;

        public int RolesID { get; set; }

        public Roles()
        {
            this.user = new HashSet<User>();
        }


        public string TypeRole { get; set; }

        public ISet<User> Users
        {
            get { return this.user; }

            set { this.user = value; }
        }
    }
}
