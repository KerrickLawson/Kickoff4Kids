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
    public class ReportViewModel
    {
        [DisplayName("School Name")]
        public string SchoolName { get; set; }

        public string State { get; set; }
        [DisplayName("Unspent Student Points")]
        public int Total_Unspent_Student_Points { get; set; }
        [DisplayName("Accumulated Student Points")]
        public int Total_Accumulated_Student_Points { get; set; }
        [DisplayName("First Name")]
        public string StudentFirstName { get; set; }
        [DisplayName("Last Name")]
        public string StudentLastName { get; set; }
        [DisplayName("Student Points")]
        public int? StudentPoints { get; set; }
        [DisplayName("Total Points Per School")]
        public int TotalPointsPerSchool { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [DisplayName("Total Participating Students")]
        public int? Total_Participating_Students { get; set; }

    }
}