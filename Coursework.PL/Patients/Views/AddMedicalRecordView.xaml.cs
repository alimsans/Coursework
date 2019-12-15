using System;
using System.Windows;

namespace Coursework.PL.Patients.Views
{
    /// <summary>
    /// Interaction logic for AddMedicalRecordView.xaml
    /// </summary>
    public partial class AddMedicalRecordView : Window
    {
        internal DateTime From;
        internal DateTime Until;
        internal string Diagnosis;
        internal bool IsConfirmedToAdd;
        public AddMedicalRecordView()
        {
            InitializeComponent();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(Diagnosis_TextBox.Text))
            {
                MessageBox.Show("Diagnosis cannot be empty");
                return;
            }
            if (!DateTime.TryParse(From_TextBox.Text, out From))
            {
                MessageBox.Show("Wrong date format in \"From\"");
                return;
            }
            if (!DateTime.TryParse(Until_TextBox.Text, out Until))
            {
                MessageBox.Show("Wrong date format in \"Until\"");
                return;
            }


            Diagnosis = Diagnosis_TextBox.Text;
            IsConfirmedToAdd = true;
            Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
