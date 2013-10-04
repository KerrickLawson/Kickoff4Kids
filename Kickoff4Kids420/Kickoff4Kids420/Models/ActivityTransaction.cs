using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Kickoff4Kids420.Models
{
    public class ActivityTransaction
    {
        public int ActivityTransactionId { get; set; }
        [DisplayName("Student")]
        public int UserId { get; set; }
        public virtual UserProfile UserProfiles { get; set; }
        [DisplayName("Activity Date")]
        public DateTime? ActivityDate { get; set; }

        public int ActivityId { get; set; }
        
        public virtual Activity Activity { get; set; }
        
    }
}