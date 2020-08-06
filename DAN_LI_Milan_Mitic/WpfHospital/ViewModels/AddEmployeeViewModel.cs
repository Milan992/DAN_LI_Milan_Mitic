using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfHospital.Model;
using WpfHospital.Views;

namespace WpfHospital.ViewModels
{
    class AddEmployeeViewModel : ViewModelBase
    {
        AddEmployee addEmployee;
        Service service = new Service();

        #region Constructors

        public AddEmployeeViewModel(AddEmployee addEmployeeOpen)
        {
            account = new tblAccount();
            employee = new tblEmployee();
            doctor = new vwDoctor();
            DoctorList = service.GetAllDoctors();
            addEmployee = addEmployeeOpen;
        }

        #endregion

        #region Properties

        private tblAccount account;

        public tblAccount Account
        {
            get
            {
                return account;
            }
            set
            {
                account = value;
                OnPropertyChanged("Account");
            }
        }

        private tblEmployee employee;

        public tblEmployee Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
            }
        }

        private vwDoctor doctor;

        public vwDoctor Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                doctor = value;
                OnPropertyChanged("Doctor");
            }
        }

        private List<vwDoctor> doctorList;

        public List<vwDoctor> DoctorList
        {
            get { return doctorList; }
            set
            {
                doctorList = value;
                OnPropertyChanged("DoctorList");
            }
        }


        #endregion

        #region Commands

        private ICommand save;

        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }

                return save;
            }
        }

        private void SaveExecute()
        {
            try
            {
                service.AddEmployee(Account, Employee, Doctor);
                MessageBox.Show("Employee saved.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute()
        {
            return true;
        }

        private ICommand close;

        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }

                return close;
            }
        }

        private void CloseExecute()
        {
            try
            {
                addEmployee.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanCloseExecute()
        {
            return true;
        }

        #endregion
    }
}
