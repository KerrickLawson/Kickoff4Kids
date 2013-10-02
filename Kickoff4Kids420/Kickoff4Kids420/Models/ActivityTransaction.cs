using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kickoff4Kids420.Models
{
    public class ActivityTransaction
    {
        public int ActivityTransactionId { get; set; }
        public int UserId { get; set; }       
        public DateTime? ActivityDate { get; set; }
        public virtual Activity Activity { get; set; }
        
    }
}