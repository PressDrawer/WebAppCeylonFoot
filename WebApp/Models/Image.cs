using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public byte[] Imagestore { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }

    /*    internal static void CopyTo(MemoryStream ms)
        {
            throw new NotImplementedException();
        } */
    }
}
