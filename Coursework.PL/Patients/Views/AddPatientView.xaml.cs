using System.Windows;

namespace Coursework.PL.Views.Patients
{
    /// <summary>
    /// Interaction logic for AddPatientView.xaml
    /// </summary>
    public partial class AddPatientView : Window
    {
        internal bool IsConfirmedToAdd;

        public AddPatientView()
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

            IsConfirmedToAdd = true;
            Close();
        }
        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
