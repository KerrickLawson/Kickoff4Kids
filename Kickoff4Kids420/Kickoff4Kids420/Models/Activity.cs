using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kickoff4Kids420.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string Description { get; set; }
        public int PointValue { get; set; }
    }
}