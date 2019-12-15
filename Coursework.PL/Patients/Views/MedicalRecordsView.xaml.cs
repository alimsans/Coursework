using Coursework.PL.Patients.Views;
using Coursework.PL.ViewModels;
using Coursework.Types;
using System.Windows;
using System.Windows.Controls;


namespace Coursework.PL.Views.Patients
{
    /// <summary>
    /// Interaction logic for MedicalRecordsView.xaml
    /// </summary>
    public partial class MedicalRecordsView : Window
    {
        private MedicalRecordsViewModel _medicalRecordsViewModel;
        public MedicalRecordsView(Patient patient)
        {
            this.InitializeComponent();
            this._medicalRecordsViewModel = new MedicalRecordsViewModel(patient);
            this.MedicalRecords_DataGrid.ItemsSource = this._medicalRecordsViewModel.MedicalRecords;
            this._medicalRecordsViewModel.UpdateMedicalRecordsAsync().ConfigureAwait(false);
        }

        private void MedicalRecords_DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            this._medicalRecordsViewModel.SelectedMedicalRecord = dataGrid.SelectedItem as MedicalRecord;
        }

        private async void AddMedicalRecord_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            AddMedicalRecordView addMedicalRecordView = new AddMedicalRecordView();
            addMedicalRecordView.ShowDialog();

            if (!addMedicalRecordView.IsConfirmedToAdd)
                return;

            MedicalRecord record = new MedicalRecord
                (this._medicalRecordsViewModel.SelectedPatient,
                addMedicalRecordView.From,
                addMedicalRecordView.Until,
                addMedicalRecordView.Diagnosis);
            await this._medicalRecordsViewModel.AddMedicalRecordAsync(record);
        }

        private async void RemoveMedicalRecord_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if (this._medicalRecordsViewModel.SelectedMedicalRecord == null)
                return;

            await this._medicalRecordsViewModel.RemoveMedicalRecordAsync(this._medicalRecordsViewModel.SelectedMedicalRecord);
        }
    }
}
