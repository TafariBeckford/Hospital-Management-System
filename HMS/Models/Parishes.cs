using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class Parishes
    {
        public int ID { get; set; }
        public string Parish { get; set; }
    }

    public class UserTypes
    {
        public int ID { get; set; }

        public string UserType {get; set; }


    }

    //public class Day
    //{
    //    public int ID { get; set; }

    //    public string Days { get; set; }
    //}

    public enum Day
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday,
    }

    public enum UserType
    {
        Administrator,
        MedicalStaff,
        Receptionist,
        AlliedHealth,

    }

    public enum Position
    {
        ClinicalAssistants,
        PatientServicesAssistants,
        Porters,
        Volunteers,
        WardClerk,
        AncillaryStaff,
        Security

    }


}