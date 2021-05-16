using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Services
{
    public class DoctorServices
    {
        public static List<ApplicationUser> GetDoctors()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Users.Where(u => u.UserType == UserType.MedicalStaff.ToString()).ToList();
            }
        }

        public static List<Department> GetDepartment()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Departments.ToList();
            }
        }
    }
}