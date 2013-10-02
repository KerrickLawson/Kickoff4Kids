using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kickoff4Kids420.Models
{
    public class School
    {
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public string SchoolAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public int PhoneNumber { get; set; }
    }
}