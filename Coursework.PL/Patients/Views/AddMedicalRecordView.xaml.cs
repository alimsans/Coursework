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
            this.InitializeComponent();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(this.Diagnosis_TextBox.Text))
            {
                MessageBox.Show("Diagnosis cannot be empty");
                return;
            }
            if (!DateTime.TryParse(this.From_TextBox.Text, out this.From))
            {
                MessageBox.Show("Wrong date format in \"From\"");
                return;
            }
            if (!DateTime.TryParse(this.Until_TextBox.Text, out this.Until))
            {
                MessageBox.Show("Wrong date format in \"Until\"");
                return;
            }


            this.Diagnosis = this.Diagnosis_TextBox.Text;
            this.IsConfirmedToAdd = true;
            this.Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
