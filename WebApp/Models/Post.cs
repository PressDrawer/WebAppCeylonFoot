using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Identity.Data;

namespace WebApp.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Contact { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public AppUser AppUser { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }

        public ICollection<Review> Reviews { get; set; }
      
        public ICollection<Image> Images { get; set; }

    }
}
