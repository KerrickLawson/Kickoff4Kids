using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Kickoff4Kids420.Models
{
    
    public class School
    {
        
        public int SchoolId { get; set; }
        [DisplayName("School Name")]
        public string SchoolName { get; set; }
        [DisplayName("School Address")]
        
        public string SchoolAddress { get; set; }     
        public string City { get; set; }
        public string State { get; set; }
        [DisplayName("Zip Code")]
        public int ZipCode { get; set; }
        [DisplayName("Phone Number")]
        public int PhoneNumber { get; set; }
    }
}