﻿using System.ComponentModel;

namespace ExtRS.Portal.Models
{
    public class UserModel
    {
        public Guid UserID { get; set; }

        public int UserType { get; set; }

        public int AuthType { get; set; }

        public String UserName { get; set; }
    }
}