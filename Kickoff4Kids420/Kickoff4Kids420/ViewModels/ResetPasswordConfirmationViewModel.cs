using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kickoff4Kids420.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kickoff4Kids420.ViewModels
{
    public class ResetPasswordConfirmationViewModel
    {
       
        public string resetPasswordToken { get; set; }
        
        public string password { get; set; }
        [Compare("password")]
        public string confirmPassword { get; set; }
        public string UserName { get; set; }


    }
}