using Coursework.PL.Doctors.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Coursework.PL.Doctors.Views
{
    /// <summary>
    /// Interaction logic for DoctorsPage.xaml
    /// </summary>
    public partial class DoctorsPage : Page
    {
        DoctorsViewModel _doctorsViewModel;
        public DoctorsPage()
        {
            InitializeComponent();

            _doctorsViewModel = new DoctorsViewModel();
            Doctors_DataGrid.ItemsSource = _doctorsViewModel.Doctors;
            _doctorsViewModel.UpdateDoctorsAsync().ConfigureAwait(false);

        }

        private async void AddDoctor_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            AddDoctorView addDoctorView = new AddDoctorView();
            addDoctorView.ShowDialog();

            if (!addDoctorView.IsConfirmedToAdd)
                return;

            Doctor doctor = new Doctor
                (addDoctorView.FirstName_TextBox.Text,
                addDoctorView.LastName_TextBox.Text,
                addDoctorView.Occupation_TextBox.Text);

            await _doctorsViewModel.AddDoctorAsync(doctor);
        }
        private void Doctors_DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            _doctorsViewModel.SelectedDoctor = dataGrid.SelectedItem as Doctor;
        }

        private async void EditDoctor_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if (_doctorsViewModel.SelectedDoctor == null)
                return;

            EditDoctorView editDoctorView = new EditDoctorView(_doctorsViewModel.SelectedDoctor);
            editDoctorView.ShowDialog();

            if (!editDoctorView.IsEdited)
                return;

            await _doctorsViewModel.EditDoctorAsync(_doctorsViewModel.SelectedDoctor, editDoctorView.NewDoctor);
        }
        private async void RemoveDoctor_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if (_doctorsViewModel.SelectedDoctor == null)
                return;

            await _doctorsViewModel.RemoveDoctorAsync(_doctorsViewModel.SelectedDoctor);
        }
        private async void RefreshDoctors_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            await _doctorsViewModel.UpdateDoctorsAsync();
        }

        private async void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.FirstName_TextBox.Text) ||
                string.IsNullOrEmpty(this.LastName_TextBox.Text))
                return;

            await _doctorsViewModel.SearchDoctorsByNameAsync
                (this.FirstName_TextBox.Text, this.LastName_TextBox.Text);
        }


    }
}
