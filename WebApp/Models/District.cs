using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class District
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
