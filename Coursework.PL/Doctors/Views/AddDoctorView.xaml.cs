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
            InitializeComponent();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FirstName_TextBox.Text)
                || string.IsNullOrEmpty(LastName_TextBox.Text))
            {
                MessageBox.Show("Name cannot be empty.");
                return;
            }
            if (string.IsNullOrEmpty(Occupation_TextBox.Text))
            {
                MessageBox.Show("Occupation cannot be empty.");
                return;
            }

            IsConfirmedToAdd = true;
            Close();
        }
        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
