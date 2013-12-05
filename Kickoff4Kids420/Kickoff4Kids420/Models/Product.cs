using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Kickoff4Kids420.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public string PictureUrl { get; set; }
        public virtual Category Categories{ get; set; }

        
        
    }
}