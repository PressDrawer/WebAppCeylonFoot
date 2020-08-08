﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApp.Models;

namespace WebApp.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the AppUser class
    public class AppUser : IdentityUser
    {
        public ICollection<Post> Posts { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
