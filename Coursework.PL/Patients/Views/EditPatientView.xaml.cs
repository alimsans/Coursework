using Coursework.Types;
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
            this.Id_TextBlock.Text = oldPatient.Id.ToString();
            this.FirstName_TextBox.Text = oldPatient.FirstName;
            this.LastName_TextBox.Text = oldPatient.LastName;
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_oldPatient.FirstName == this.FirstName_TextBox.Text && _oldPatient.LastName == this.LastName_TextBox.Text)
                this.Close();

            if (string.IsNullOrEmpty(this.FirstName_TextBox.Text) || string.IsNullOrEmpty(this.LastName_TextBox.Text))
            {
                MessageBox.Show("Name cannot be empty.");
                return;
            }

            NewPatient = new Patient(this.FirstName_TextBox.Text, this.LastName_TextBox.Text);
            IsEdited = true;

            this.Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
