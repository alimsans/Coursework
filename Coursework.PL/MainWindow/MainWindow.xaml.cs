using Coursework.PL.Appointments.Views;
using Coursework.PL.Doctors.Views;
using Coursework.PL.Views;
using System.Windows;

namespace Coursework.PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PatientsPage _patientsPage;
        private DoctorsPage _doctorsPage;
        private AppointmentsPage _appointmentsPage;
        public MainWindow()
        {
            this.InitializeComponent();
            this._patientsPage = new PatientsPage();
            this._doctorsPage = new DoctorsPage();
            this._appointmentsPage = new AppointmentsPage();

            this.Main_Frame.Navigate(_patientsPage);
        }

        private void Patients_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Main_Frame.Navigate(this._patientsPage);
        }

        private void Doctors_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Main_Frame.Navigate(this._doctorsPage);
        }

        private void Appointments_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Main_Frame.Navigate(this._appointmentsPage);
        }
    }
}
