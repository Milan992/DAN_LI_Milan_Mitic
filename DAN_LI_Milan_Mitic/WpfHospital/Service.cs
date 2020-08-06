using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfHospital.Model;

namespace WpfHospital
{
    class Service
    {
        /// <summary>
        /// Checks if employee with the username and pass exists in the database.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        internal bool IsEmployee(string userName, string password)
        {
            try
            {
                using (HospitalEntities context = new HospitalEntities())
                {
                    tblEmployee employee = (from e in context.tblEmployees where e.UserName == userName && e.Pass == password select e).First();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Checks if doctor with the username and pass exists in the database.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        internal bool IsDoctor(string userName, string password)
        {
            try
            {
                using (HospitalEntities context = new HospitalEntities())
                {
                    tblDoctor doctor = (from d in context.tblDoctors where d.UserName == userName && d.Pass == password select d).First();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Returns a employee with the username from database.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        internal tblEmployee GetEmployee(string userName)
        {
            tblEmployee employee = new tblEmployee();
            using (HospitalEntities context = new HospitalEntities())
            {
                 employee = (from e in context.tblEmployees where e.UserName == userName select e).First();
            }
            return employee;
        }

        /// <summary>
        /// Returns a doctor with the username from database
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        internal tblDoctor GetDoctor(string userName)
        {
            tblDoctor doctor = new tblDoctor();
            using (HospitalEntities context = new HospitalEntities())
            {
                 doctor = (from d in context.tblDoctors where d.UserName == userName select d).First();
            }
            return doctor;
        }
    }
}
