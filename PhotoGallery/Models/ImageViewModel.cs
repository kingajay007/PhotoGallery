using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhotoGallery.Models
{
    public class Picture
    {
       
        public int Id { get; set; }

        [NotMapped]
        public HttpPostedFileBase Image { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public string Path { get; set; }
    }

    public class ImageStore
    {

        public int Id { get; set; }
        public HttpPostedFile Image { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public string Path { get; set; }

    }
}