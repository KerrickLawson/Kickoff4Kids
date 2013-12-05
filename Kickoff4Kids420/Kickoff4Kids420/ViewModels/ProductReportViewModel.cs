using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kickoff4Kids420.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kickoff4Kids420.ViewModels
{
    public class ProductReportViewModel
    {
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        public int Price { get; set; }
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
        [DisplayName("Order Count")]
        public int ProductOrderedCount { get; set; }

    }
}
