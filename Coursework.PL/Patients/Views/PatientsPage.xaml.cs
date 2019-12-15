using Coursework.PL.ViewModels;
using Coursework.PL.Views.Patients;
using Coursework.Types;
using System.Windows;
using System.Windows.Controls;

namespace Coursework.PL.Views
{
    /// <summary>
    /// Interaction logic for PatientsPage.xaml
    /// </summary>
    public partial class PatientsPage : Page
    {
        private PatientsViewModel _patientsViewModel;

        public PatientsPage()
        {
            this.InitializeComponent();

            this._patientsViewModel = new PatientsViewModel();
            this.Patients_DataGrid.ItemsSource = this._patientsViewModel.Patients;
            this._patientsViewModel.UpdatePatientsAsync().ConfigureAwait(false);
        }


        private void Patients_DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            this._patientsViewModel.SelectedPatient = dataGrid.SelectedItem as Patient;
        }

        private async void AddPatient_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            AddPatientView addPatientView = new AddPatientView();
            addPatientView.ShowDialog();

            if (!addPatientView.IsConfirmedToAdd)
                return;

            Patient patient = new Patient
                (addPatientView.FirstName_TextBox.Text, addPatientView.LastName_TextBox.Text);
            await this._patientsViewModel.AddPatientAsync(patient).ConfigureAwait(false);
        }

        private async void EditPatient_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if (this._patientsViewModel.SelectedPatient == null)
                return;

            Patient oldPatient = this._patientsViewModel.SelectedPatient;
            EditPatientView editPatientView = new EditPatientView(oldPatient);
            editPatientView.ShowDialog();

            if (editPatientView.IsEdited)
                await this._patientsViewModel.EditPatientAsync(oldPatient, editPatientView.NewPatient);
        }

        private async void RemovePatient_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if (this._patientsViewModel.SelectedPatient == null)
                return;

            MessageBoxResult messageBoxResult = MessageBox.Show(
                messageBoxText: $"Are you sure you want to remove {this._patientsViewModel.SelectedPatient.FirstName} {this._patientsViewModel.SelectedPatient.LastName}?",
                caption: "Remove",
                MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.No)
                return;

            await this._patientsViewModel.RemovePatientAsync(this._patientsViewModel.SelectedPatient);
        }

        private void ShowMedicalRecords_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if (this._patientsViewModel.SelectedPatient == null)
                return;

            MedicalRecordsView medicalRecordsView = new MedicalRecordsView(this._patientsViewModel.SelectedPatient);
            medicalRecordsView.ShowDialog();
        }

        private async void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.FirstName_TextBox.Text) ||
                string.IsNullOrEmpty(this.LastName_TextBox.Text))
                return;

            await this._patientsViewModel.SearchPatientsByNameAsync
                (this.FirstName_TextBox.Text, this.LastName_TextBox.Text);
        }

        private async void RefreshPatients_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            await this._patientsViewModel.UpdatePatientsAsync();
        }
    }
}
