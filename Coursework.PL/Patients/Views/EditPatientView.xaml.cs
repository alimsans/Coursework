using Coursework.Types;
using System.Windows;

namespace Coursework.PL.Views.Patients
{
    /// <summary>
    /// Interaction logic for EditPatientView.xaml
    /// </summary>
    public partial class EditPatientView : Window
    {
        private Patient _oldPatient;

        public bool IsEdited;
        public Patient NewPatient { get; private set; }
        public EditPatientView(Patient oldPatient)
        {
            this.InitializeComponent();
            this._oldPatient = oldPatient;
            this.Id_TextBlock.Text = oldPatient.Id.ToString();
            this.FirstName_TextBox.Text = oldPatient.FirstName;
            this.LastName_TextBox.Text = oldPatient.LastName;
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this._oldPatient.FirstName == this.FirstName_TextBox.Text && this._oldPatient.LastName == this.LastName_TextBox.Text)
                this.Close();

            if (string.IsNullOrEmpty(this.FirstName_TextBox.Text) || string.IsNullOrEmpty(this.LastName_TextBox.Text))
            {
                MessageBox.Show("Name cannot be empty.");
                return;
            }

            this.NewPatient = new Patient(this.FirstName_TextBox.Text, this.LastName_TextBox.Text);
            this.IsEdited = true;

            this.Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
