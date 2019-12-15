using Coursework.PL.Doctors.ViewModels;
using Coursework.Types;
using System.Windows;
using System.Windows.Controls;

namespace Coursework.PL.Doctors.Views
{
    /// <summary>
    /// Interaction logic for DoctorsPage.xaml
    /// </summary>
    public partial class DoctorsPage : Page
    {
        private DoctorsViewModel _doctorsViewModel;
        public DoctorsPage()
        {
            this.InitializeComponent();

            this._doctorsViewModel = new DoctorsViewModel();
            this.Doctors_DataGrid.ItemsSource = this._doctorsViewModel.Doctors;
            this._doctorsViewModel.UpdateDoctorsAsync().ConfigureAwait(false);
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

            await this._doctorsViewModel.AddDoctorAsync(doctor);
        }

        private void Doctors_DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            this._doctorsViewModel.SelectedDoctor = dataGrid.SelectedItem as Doctor;
        }

        private async void EditDoctor_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if (this._doctorsViewModel.SelectedDoctor == null)
                return;

            EditDoctorView editDoctorView = new EditDoctorView(this._doctorsViewModel.SelectedDoctor);
            editDoctorView.ShowDialog();

            if (!editDoctorView.IsEdited)
                return;

            await this._doctorsViewModel.EditDoctorAsync(this._doctorsViewModel.SelectedDoctor, editDoctorView.NewDoctor);
        }

        private async void RemoveDoctor_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if (this._doctorsViewModel.SelectedDoctor == null)
                return;

            await this._doctorsViewModel.RemoveDoctorAsync(this._doctorsViewModel.SelectedDoctor);
        }

        private async void RefreshDoctors_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            await this._doctorsViewModel.UpdateDoctorsAsync();
        }

        private async void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.FirstName_TextBox.Text) ||
                string.IsNullOrEmpty(this.LastName_TextBox.Text))
                return;

            await this._doctorsViewModel.SearchDoctorsByNameAsync
                (this.FirstName_TextBox.Text, this.LastName_TextBox.Text);
        }
    }
}
