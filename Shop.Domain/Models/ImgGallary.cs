using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models
{
   public class ImgGallary
    {
        public int Id { get; set; }
        public string GallaryImgUrl { get; set; }


        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
