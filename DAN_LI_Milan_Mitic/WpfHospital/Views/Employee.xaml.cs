using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfHospital.ViewModels;

namespace WpfHospital.Views
{
    /// <summary>
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : Window
    {
        public Employee()
        {
            InitializeComponent();
            this.DataContext = new EmployeeViewModel(this);
        }

        public Employee(string userName)
        {
            InitializeComponent();
            this.DataContext = new EmployeeViewModel(this, userName);
        }
    }
}
