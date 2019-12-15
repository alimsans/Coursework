using System;
using System.Windows;

namespace Coursework.PL.Appointments.Views
{
    /// <summary>
    /// Interaction logic for AddAppointmentView.xaml
    /// </summary>
    public partial class AddAppointmentView : Window
    {
        internal bool IsConfirmed { get; private set; }

        private int _doctorId;
        private int _patientId;
        private DateTime _dateTime;
        internal int DoctorId { get => _doctorId; private set => _doctorId = value; }
        internal int PatientId { get => _patientId; private set => _patientId = value; }
        internal DateTime DateTime { get => _dateTime; private set => _dateTime = value; }
        public AddAppointmentView()
        {
            InitializeComponent();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(DoctorId_TextBox.Text)
                || string.IsNullOrEmpty(PatientId_TextBox.Text))
            {
                MessageBox.Show("Id cannot be empty.");
                return;
            }

            if (!int.TryParse(DoctorId_TextBox.Text, out _doctorId) ||
                !int.TryParse(PatientId_TextBox.Text, out _patientId))
            {
                MessageBox.Show("Invalid id format.");
                return;
            }

            if (string.IsNullOrEmpty(DateTime_TextBox.Text))
            {
                MessageBox.Show("Date time cannot be empty.");
                return;
            }

            if (!DateTime.TryParse(DateTime_TextBox.Text, out _dateTime))
            {
                MessageBox.Show("Try yyyy-mm-dd hh:mm", "Invalid date time format.");
                return;
            }

            IsConfirmed = true;
            Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
