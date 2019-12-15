using System.Windows;

namespace Coursework.PL.Doctors.Views
{
    /// <summary>
    /// Interaction logic for AddDoctorView.xaml
    /// </summary>
    public partial class AddDoctorView : Window
    {
        internal bool IsConfirmedToAdd;
        public AddDoctorView()
        {
            this.InitializeComponent();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.FirstName_TextBox.Text)
                || string.IsNullOrEmpty(this.LastName_TextBox.Text))
            {
                MessageBox.Show("Name cannot be empty.");
                return;
            }
            if (string.IsNullOrEmpty(this.Occupation_TextBox.Text))
            {
                MessageBox.Show("Occupation cannot be empty.");
                return;
            }

            this.IsConfirmedToAdd = true;
            this.Close();
        }
        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
