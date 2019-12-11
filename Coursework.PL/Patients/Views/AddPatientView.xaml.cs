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
            if (string.IsNullOrEmpty(this.FirstName_TextBox.Text) 
                || string.IsNullOrEmpty(this.LastName_TextBox.Text))
            {
                MessageBox.Show("Name cannot be empty.");
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
