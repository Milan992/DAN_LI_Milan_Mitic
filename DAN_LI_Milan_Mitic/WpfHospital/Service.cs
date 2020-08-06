using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfHospital.Model;

namespace WpfHospital
{
    class Service
    {
        /// <summary>
        /// Checks if string is in JMBG format.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsJmbg(string userName)
        {
            bool jmbg = false;
            if (userName.Length == 13)
            {
                try
                {
                    long i = Convert.ToInt64(userName);
                    string date = "1" + userName.Substring(4, 3) + "-" + userName.Substring(2, 2) + "-" + userName.Substring(0, 2);
                    DateTime dateOfBirth = DateTime.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    jmbg = true;
                }
                catch
                {
                    jmbg = false;
                }
            }
            else
            {
                jmbg = false;
            }
            return jmbg;
        }

        internal List<tblRequest> GetAllRequests(tblDoctor doctor)
        {
            using (HospitalEntities1 context = new HospitalEntities1())
            {
                try
                {
                    List<tblEmployee> employees = (from a in context.tblEmployees where a.DoctorID == doctor.DoctorID select a).ToList();
                    List<tblRequest> requests = new List<tblRequest>();
                    foreach (var item in employees)
                    {
                        try
                        {
                            List<tblRequest> requestsByEmployee = new List<tblRequest>();
                            requestsByEmployee = (from r in context.tblRequests where r.EmployeeID == item.EmployeeID select r).ToList();
                            foreach (tblRequest r in requestsByEmployee)
                            {
                                requests.Add(r);
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                    return requests;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return null;
                }
            }
        }

        internal List<vwDoctor> GetAllDoctors()
        {
            try
            {
                using (HospitalEntities1 context = new HospitalEntities1())
                {
                    List<vwDoctor> list = (from d in context.vwDoctors select d).ToList();
                    if (list.Count == 0)
                    {
                        MessageBox.Show("Warning!\nYou can not register any employees untill there is no doctors registrated");

                    }
                    return list;
                }
            }
            catch
            {
                MessageBox.Show("You can not register any employees untill there is no doctors registrated");
                return null;
            }
        }

        internal void AddRequest(tblEmployee employeeToView, tblRequest request)
        {
            using (HospitalEntities1 context = new HospitalEntities1())
            {
                tblRequest newRequest = new tblRequest();
                newRequest.EmployeeID = employeeToView.EmployeeID;
                newRequest.Reason = request.Reason;
                newRequest.RequestDate = DateTime.Today;

                newRequest.CompanyName = request.CompanyName;
                newRequest.Emergent = request.Emergent;
                newRequest.Approved = false;
                context.tblRequests.Add(newRequest);
                context.SaveChanges();
            }
        }

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
                using (HospitalEntities1 context = new HospitalEntities1())
                {
                    tblAccount account = (from a in context.tblAccounts where a.UserName == userName && a.Pass == password select a).First();
                    tblEmployee employee = (from e in context.tblEmployees where e.AccountID == account.AccountID select e).First();
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
                using (HospitalEntities1 context = new HospitalEntities1())
                {
                    tblAccount account = (from a in context.tblAccounts where a.UserName == userName && a.Pass == password select a).First();
                    tblDoctor doctor = (from e in context.tblDoctors where e.AccountID == account.AccountID select e).First();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Adds employee to database.
        /// </summary>
        /// <param name="doctor"></param>
        internal void AddDoctor(tblAccount account, tblDoctor doctor)
        {
            using (HospitalEntities1 context = new HospitalEntities1())
            {
                tblAccount newAccount = new tblAccount();
                newAccount.FullName = account.FullName;
                newAccount.JMBG = account.JMBG;
                newAccount.UserName = account.UserName;
                newAccount.Pass = account.Pass;
                context.tblAccounts.Add(newAccount);
                context.SaveChanges();

                tblDoctor newDoctor = new tblDoctor();
                newDoctor.AccountID = newAccount.AccountID;
                newDoctor.BankAccount = doctor.BankAccount;
                context.tblDoctors.Add(newDoctor);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Adds doctor to database.
        /// </summary>
        /// <param name="employee"></param>
        internal void AddEmployee(tblAccount account, tblEmployee employee, vwDoctor doctor)
        {
            using (HospitalEntities1 context = new HospitalEntities1())
            {
                tblAccount newAccount = new tblAccount();
                newAccount.FullName = account.FullName;
                newAccount.JMBG = account.JMBG;
                newAccount.UserName = account.UserName;
                newAccount.Pass = account.Pass;
                context.tblAccounts.Add(newAccount);
                context.SaveChanges();

                tblEmployee newEmployee = new tblEmployee();
                newEmployee.AccountID = newAccount.AccountID;
                newEmployee.InsuranceCardNumber = employee.InsuranceCardNumber;
                newEmployee.DoctorID = doctor.DoctorID;
                context.tblEmployees.Add(newEmployee);
                context.SaveChanges();
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
            using (HospitalEntities1 context = new HospitalEntities1())
            {
                tblAccount account = (from a in context.tblAccounts where a.UserName == userName select a).First();
                employee = (from e in context.tblEmployees where e.AccountID == account.AccountID select e).First();
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
            using (HospitalEntities1 context = new HospitalEntities1())
            {
                tblAccount account = (from a in context.tblAccounts where a.UserName == userName select a).First();
                doctor = (from e in context.tblDoctors where e.AccountID == account.AccountID select e).First();
            }
            return doctor;
        }
    }
}
