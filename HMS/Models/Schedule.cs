using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class Schedule
    {
        
        public int ID { get; set; }

        [Required]
        public string DoctorName { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public bool Status { get; set; }

        public string Message { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm tt}")]
        public DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm tt}")]
        public DateTime EndTime { get; set; }

        [Required]
        public string AvailableDays { get; set; }
    }
}