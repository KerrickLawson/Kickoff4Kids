using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kickoff4Kids420.Models
{
    [Bind(Exclude = "OrderId")]
    public partial class Order
    {
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }
        [ScaffoldColumn(false)]
        public int UserId { get; set; }
        [ScaffoldColumn(false)]
        public string UserName { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Order Date")]
        public System.DateTime OrderDate { get; set; }
        [DisplayName("Product Name")]
        [ScaffoldColumn(false)]
        public int Total { get; set; }
        public bool IsFulfilled { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public virtual UserProfile UserProfiles{ get; set; }

        

    }
}