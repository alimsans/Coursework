using Coursework.PL.Doctors.Views;
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
        DoctorsPage _doctorsPage;
        public MainWindow()
        {
            InitializeComponent();
            _patientsPage = new PatientsPage();
            _doctorsPage = new DoctorsPage();
        }

        private void Patients_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Frame.Navigate(_patientsPage);
        }

        private void Doctors_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Frame.Navigate(_doctorsPage);
        }

        private void Appointments_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
