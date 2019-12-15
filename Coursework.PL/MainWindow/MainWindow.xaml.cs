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
            InitializeComponent();
            _patientsPage = new PatientsPage();
            _doctorsPage = new DoctorsPage();
            _appointmentsPage = new AppointmentsPage();

            Main_Frame.Navigate(_patientsPage);
        }

        private void Patients_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Frame.Navigate(_patientsPage);
        }

        private void Doctors_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Frame.Navigate(_doctorsPage);
        }

        private void Appointments_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Frame.Navigate(_appointmentsPage);
        }
    }
}
