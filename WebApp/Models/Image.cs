using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public byte[] Imagestore { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
