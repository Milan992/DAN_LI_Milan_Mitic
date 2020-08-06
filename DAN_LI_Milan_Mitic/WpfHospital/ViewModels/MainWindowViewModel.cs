using System;
using System.Windows;
using System.Windows.Input;
using WpfHospital.Views;

namespace WpfHospital.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        Service service = new Service();
        MainWindow main;

        #region Constructors

        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
        }

        #endregion

        #region Properties

        private string userName;

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        private string password;

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        #endregion

        #region Commands

        private ICommand logIn;

        public ICommand LogIn
        {
            get
            {
                if (logIn == null)
                {
                    logIn = new RelayCommand(param => LogInExecute(), param => CanLogInExecute());
                }

                return logIn;
            }
        }

        private void LogInExecute()
        {
            try
            {
                if (service.IsEmployee(UserName, Password))
                {
                    Employee employee = new Employee(UserName);
                    employee.ShowDialog();
                }
                else if(service.IsDoctor(UserName, Password))
                {
                    Doctor doctor = new Doctor(UserName);
                    doctor.ShowDialog();
                }
                else{
                    MessageBox.Show("Incorrect Username or Password.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanLogInExecute()
        {
            return true;
        }

        private ICommand mewAccount;

        public ICommand NewAccount
        {
            get
            {
                if (mewAccount == null)
                {
                    mewAccount = new RelayCommand(param => NewAccountExecute(), param => CanNewAccountExecute());
                }

                return mewAccount;
            }
        }

        private void NewAccountExecute()
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanNewAccountExecute()
        {
            return true;
        }

        #endregion
    }
}
