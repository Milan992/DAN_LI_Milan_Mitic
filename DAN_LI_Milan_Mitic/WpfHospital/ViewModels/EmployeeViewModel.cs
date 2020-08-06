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
    class EmployeeViewModel : ViewModelBase
    {
        Employee employee;
        Service service = new Service();

        #region Constructors

        public EmployeeViewModel(Employee employeeOpen)
        {
            employee = employeeOpen;
        }

        public EmployeeViewModel(Employee employeeOpen, string userName)
        {
            employee = employeeOpen;
            request = new tblRequest();
            employeeToView = service.GetEmployee(userName);
        }

        #endregion

        #region Properties

        private tblEmployee employeeToView;

        public tblEmployee EmployeeToView
        {
            get
            {
                return employeeToView;
            }
            set
            {
                employeeToView = value;
                OnPropertyChanged("EmployeeToView");
            }
        }

        private tblRequest request;

        public tblRequest Request
        {
            get { return request; }
            set
            {
                request = value; OnPropertyChanged("Request");
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
                service.AddRequest(EmployeeToView, Request);
                MessageBox.Show("Request sent.");
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
                employee.Close();
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
