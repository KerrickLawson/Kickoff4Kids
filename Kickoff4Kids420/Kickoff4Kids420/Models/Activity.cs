using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Kickoff4Kids420.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        [DisplayName("Activity Name")]
        public string ActivityName { get; set; }
        public string Description { get; set; }
        [DisplayName("Point Value")]
        public int PointValue { get; set; }
    }
}