using Coursework.Types;
using System.Windows;

namespace Coursework.PL.Doctors.Views
{
    /// <summary>
    /// Interaction logic for EditDoctorView.xaml
    /// </summary>
    public partial class EditDoctorView : Window
    {
        private Doctor _oldDoctor;

        public bool IsEdited;
        public Doctor NewDoctor { get; private set; }
        public EditDoctorView(Doctor oldDoctor)
        {
            InitializeComponent();
            _oldDoctor = oldDoctor;
            Id_TextBlock.Text = oldDoctor.Id.ToString();
            FirstName_TextBox.Text = oldDoctor.FirstName;
            LastName_TextBox.Text = oldDoctor.LastName;
            Occupation_TextBox.Text = oldDoctor.Occupation;
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_oldDoctor.FirstName == FirstName_TextBox.Text &&
                _oldDoctor.LastName == LastName_TextBox.Text &&
                _oldDoctor.Occupation == Occupation_TextBox.Text)
                Close();

            if (string.IsNullOrEmpty(FirstName_TextBox.Text) || string.IsNullOrEmpty(LastName_TextBox.Text))
            {
                MessageBox.Show("Name cannot be empty.");
                return;
            }

            NewDoctor = new Doctor
                (FirstName_TextBox.Text, LastName_TextBox.Text, Occupation_TextBox.Text);
            IsEdited = true;

            Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
