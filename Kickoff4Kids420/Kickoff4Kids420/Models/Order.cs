using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kickoff4Kids420.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime OrderDate { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public virtual Product Products { get; set; }
        public virtual UserProfile UserProfiles{ get; set; }

        

    }
}