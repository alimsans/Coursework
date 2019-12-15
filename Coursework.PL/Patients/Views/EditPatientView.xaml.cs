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
            InitializeComponent();
            _oldPatient = oldPatient;
            Id_TextBlock.Text = oldPatient.Id.ToString();
            FirstName_TextBox.Text = oldPatient.FirstName;
            LastName_TextBox.Text = oldPatient.LastName;
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_oldPatient.FirstName == FirstName_TextBox.Text && _oldPatient.LastName == LastName_TextBox.Text)
                Close();

            if (string.IsNullOrEmpty(FirstName_TextBox.Text) || string.IsNullOrEmpty(LastName_TextBox.Text))
            {
                MessageBox.Show("Name cannot be empty.");
                return;
            }

            NewPatient = new Patient(FirstName_TextBox.Text, LastName_TextBox.Text);
            IsEdited = true;

            Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
