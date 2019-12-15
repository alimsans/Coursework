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
            InitializeComponent();

            _patientsViewModel = new PatientsViewModel();
            Patients_DataGrid.ItemsSource = _patientsViewModel.Patients;
            _patientsViewModel.UpdatePatientsAsync().ConfigureAwait(false);
        }


        private void Patients_DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            _patientsViewModel.SelectedPatient = dataGrid.SelectedItem as Patient;
        }

        private async void AddPatient_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            AddPatientView addPatientView = new AddPatientView();
            addPatientView.ShowDialog();

            if (!addPatientView.IsConfirmedToAdd)
                return;

            Patient patient = new Patient
                (addPatientView.FirstName_TextBox.Text, addPatientView.LastName_TextBox.Text);
            await _patientsViewModel.AddPatientAsync(patient).ConfigureAwait(false);
        }

        private async void EditPatient_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if (_patientsViewModel.SelectedPatient == null)
                return;

            Patient oldPatient = _patientsViewModel.SelectedPatient;
            EditPatientView editPatientView = new EditPatientView(oldPatient);
            editPatientView.ShowDialog();

            if (editPatientView.IsEdited)
                await _patientsViewModel.EditPatientAsync(oldPatient, editPatientView.NewPatient);
        }

        private async void RemovePatient_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if (_patientsViewModel.SelectedPatient == null)
                return;

            MessageBoxResult messageBoxResult = MessageBox.Show(
                messageBoxText: $"Are you sure you want to remove {_patientsViewModel.SelectedPatient.FirstName} {_patientsViewModel.SelectedPatient.LastName}?",
                caption: "Remove",
                MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.No)
                return;

            await _patientsViewModel.RemovePatientAsync(_patientsViewModel.SelectedPatient);
        }

        private void ShowMedicalRecords_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if (_patientsViewModel.SelectedPatient == null)
                return;

            MedicalRecordsView medicalRecordsView = new MedicalRecordsView(_patientsViewModel.SelectedPatient);
            medicalRecordsView.ShowDialog();
        }

        private async void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FirstName_TextBox.Text) ||
                string.IsNullOrEmpty(LastName_TextBox.Text))
                return;

            await _patientsViewModel.SearchPatientsByNameAsync
                (FirstName_TextBox.Text, LastName_TextBox.Text);
        }

        private async void RefreshPatients_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            await _patientsViewModel.UpdatePatientsAsync();
        }
    }
}
