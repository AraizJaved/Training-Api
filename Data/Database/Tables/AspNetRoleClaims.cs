﻿using System;
using System.Collections.Generic;

namespace Meeting_App.Data.Database.Tables
{
    public partial class AspNetRoleClaims
    {
        public int Id { get; set; }
        public Guid RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public virtual AspNetRoles Role { get; set; }
    }
}
