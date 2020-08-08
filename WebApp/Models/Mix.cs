using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Mix
    {
        public Post Post { get; set; }
        public Category Category { get; set; }
        public District District { get; set; }
    }
}
