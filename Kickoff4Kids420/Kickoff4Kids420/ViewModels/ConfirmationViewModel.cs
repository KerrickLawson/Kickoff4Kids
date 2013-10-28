using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kickoff4Kids420.Models;

namespace Kickoff4Kids420.ViewModels
{
    public class ConfirmationViewModel
    {
        public List<Cart> CartItems { get; set; }
        public int CartTotal { get; set; }
        public int PointTotal { get; set; }
        public string UserName { get; set; }
        public int Balance { get; set; }
    }
}