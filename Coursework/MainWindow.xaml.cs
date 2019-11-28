using System.Threading.Tasks;
using System.Windows;

using Coursework.BusinessLogic;
using Coursework.Types;

namespace Coursework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DoctorController _doctorController;
        private readonly AppointmentController _appointmentController;
        private readonly PatientController _patientController;

        public MainWindow()
        {
            InitializeComponent();

            _doctorController       = new DoctorController();
            _appointmentController  = new AppointmentController();
            _patientController      = new PatientController();
        }

        private  void Button_Click(object sender, RoutedEventArgs e)
        {
            var tmp = new Appointment(new Doctor("", "", ""), new Patient("", ""), System.DateTime.Now);
            this.TextBlock.Text = "Started";
            _appointmentController.AddAppointment(tmp);
            this.TextBlock.Text = "Finished";
            //Task.Run(() => _appointmentController.AddAppointment(tmp));
        }
    }
}
