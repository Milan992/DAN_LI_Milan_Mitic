using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        #endregion
    }
}
