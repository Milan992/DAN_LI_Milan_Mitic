using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfHospital.Model;
using WpfHospital.Views;

namespace WpfHospital.ViewModels
{
    class DoctorViewModel : ViewModelBase
    {
        Doctor doctor;
        Service service = new Service();

        #region Constructors

        public DoctorViewModel(Doctor doctorOpen)
        {
            doctor = doctorOpen;
        }

        public DoctorViewModel(Doctor doctorOpen, string userName)
        {
            doctor = doctorOpen;
            doctorToView = service.GetDoctor(userName);
        }

        #endregion

        #region Properties

        private tblDoctor doctorToView;

        public tblDoctor DoctorToView
        {
            get
            {
                return doctorToView;
            }
            set
            {
                doctorToView = value;
                OnPropertyChanged("DoctorToView");
            }
        }

        #endregion
    }
}
