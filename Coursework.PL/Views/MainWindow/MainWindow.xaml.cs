using Coursework.PL.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Coursework.PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PatientsPage _patientsPage;
        public MainWindow()
        {
            InitializeComponent();
            this._patientsPage = new PatientsPage();
        }

        private void Patients_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Frame.Navigate(this._patientsPage);
        }

        private void Doctors_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Appointments_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
