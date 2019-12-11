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
            this.Id_TextBlock.Text = oldDoctor.Id.ToString();
            this.FirstName_TextBox.Text = oldDoctor.FirstName;
            this.LastName_TextBox.Text = oldDoctor.LastName;
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_oldDoctor.FirstName == this.FirstName_TextBox.Text &&
                _oldDoctor.LastName == this.LastName_TextBox.Text &&
                _oldDoctor.Occupation == this.Occupation_TextBox.Text)
                this.Close();

            if (string.IsNullOrEmpty(this.FirstName_TextBox.Text) || string.IsNullOrEmpty(this.LastName_TextBox.Text))
            {
                MessageBox.Show("Name cannot be empty.");
                return;
            }

            NewDoctor = new Doctor
                (this.FirstName_TextBox.Text, this.LastName_TextBox.Text, this.Occupation_TextBox.Text);
            IsEdited = true;

            this.Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
