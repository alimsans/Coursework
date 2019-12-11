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
            InitializeComponent();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(this.Diagnosis_TextBox.Text)) 
            {
                MessageBox.Show("Diagnosis cannot be empty");
                return;
            }
            if (!DateTime.TryParse(this.From_TextBox.Text, out From)) 
            {
                MessageBox.Show("Wrong date format in \"From\"");
                return;
            }
            if (!DateTime.TryParse(this.Until_TextBox.Text, out Until)) 
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
