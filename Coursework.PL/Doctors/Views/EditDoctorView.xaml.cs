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
            this.InitializeComponent();
            this._oldDoctor = oldDoctor;
            this.Id_TextBlock.Text = oldDoctor.Id.ToString();
            this.FirstName_TextBox.Text = oldDoctor.FirstName;
            this.LastName_TextBox.Text = oldDoctor.LastName;
            this.Occupation_TextBox.Text = oldDoctor.Occupation;
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this._oldDoctor.FirstName == this.FirstName_TextBox.Text &&
                this._oldDoctor.LastName == this.LastName_TextBox.Text &&
                this._oldDoctor.Occupation == this.Occupation_TextBox.Text)
                this.Close();

            if (string.IsNullOrEmpty(this.FirstName_TextBox.Text) || string.IsNullOrEmpty(this.LastName_TextBox.Text))
            {
                MessageBox.Show("Name cannot be empty.");
                return;
            }

            this.NewDoctor = new Doctor
                (this.FirstName_TextBox.Text, this.LastName_TextBox.Text, this.Occupation_TextBox.Text);
            this.IsEdited = true;

            this.Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
