using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            if (string.IsNullOrEmpty(this.FirstName_TextBox.Text) 
                || string.IsNullOrEmpty(this.LastName_TextBox.Text))
            {
                MessageBox.Show("Name cannot be empty.");
                return;
            }
            if(string.IsNullOrEmpty(this.Occupation_TextBox.Text))
            {
                MessageBox.Show("Occupation cannot be empty.");
                return;
            }

            IsConfirmedToAdd = true;
            this.Close();
        }
        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
