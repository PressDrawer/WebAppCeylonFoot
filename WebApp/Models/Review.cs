using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Identity.Data;

namespace WebApp.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string PostReview { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
     
        public AppUser AppUser { get; set; }
    }
}
