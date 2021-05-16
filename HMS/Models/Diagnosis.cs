using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class Diagnosis
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Clinical Remarks")]
        public string ClinicalRemarks { get; set;}

        [Display(Name = "Initial Diagnosis")]
        public string InitialDiagnosis { get; set; }

        [Display(Name = "Second Diagnosis")]
        public string SecondDiagnosis { get; set; }

        [Display(Name = "Third Diagnosis")]
        public string ThirdDiagnosis { get; set; }
        public int PatientID { get; set; }
        public virtual Patient Patient { get; set; }
    }
}