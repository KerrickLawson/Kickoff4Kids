using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Kickoff4Kids420.Models
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int UserId { get; set; }
        [DisplayName("Student Name")]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }       
        public int? SchoolId { get; set; }
        public string EmailAddress { get; set; }
        //Set default value for both points
        private const int DEFAULT_VALUE = 0;
       
        [DefaultValue(DEFAULT_VALUE)]
        public int? PointTotal { get; set; }
        [DefaultValue(DEFAULT_VALUE)]
        public int? CumulativePointTotal { get; set; }

        //public string ConfirmationToken { get; set; }
        
        public virtual School Schools { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ActivityTransaction> ActivityTransactions { get; set; } 
    }
}