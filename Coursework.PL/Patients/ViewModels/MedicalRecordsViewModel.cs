using Coursework.BLL;
using Coursework.Types;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Coursework.PL.ViewModels
{
    internal class MedicalRecordsViewModel : INotifyPropertyChanged
    {
        private MedicalRecordsController _controller;
        private ObservableCollection<MedicalRecord> _medicalRecords;
        internal ObservableCollection<MedicalRecord> MedicalRecords { get => this._medicalRecords; }

        internal Patient SelectedPatient { get; set; }
        internal MedicalRecord SelectedMedicalRecord { get; set; }

        internal MedicalRecordsViewModel(Patient patient)
        {
            this.SelectedPatient = patient;
            this._medicalRecords = new ObservableCollection<MedicalRecord>();
            this._medicalRecords.CollectionChanged += this.MedicalRecords_CollectionChanged;
        }

        /// <summary>
        /// Asynchronously update Medical Records collection.
        /// </summary>
        internal async Task UpdateMedicalRecordsAsync()
        {
            List<MedicalRecord> medicalRecords = null;
            using (this._controller = new MedicalRecordsController())
                await Task.Run(
                    () => medicalRecords = (List<MedicalRecord>)this._controller.GetPatientsMedicalRecords(this.SelectedPatient));

            if (medicalRecords != null)
            {
                lock (this._medicalRecords)
                {
                    this._medicalRecords.Clear();
                    foreach (var record in medicalRecords)
                    {
                        this._medicalRecords.Add(record);
                    }
                }
            }
        }

        /// <summary>
        /// Asynchronously add a medical record
        /// </summary>
        internal async Task AddMedicalRecordAsync(MedicalRecord record)
        {
            using (this._controller = new MedicalRecordsController())
                await Task.Run(() => this._controller.AddMedicalRecord(record));

            await this.UpdateMedicalRecordsAsync();
        }

        /// <summary>
        /// Asynchronously removes a medical record
        /// </summary>
        internal async Task RemoveMedicalRecordAsync(MedicalRecord record)
        {
            using (this._controller = new MedicalRecordsController())
                await Task.Run(() => this._controller.RemoveMedicalRecord(record));

            await this.UpdateMedicalRecordsAsync();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void MedicalRecords_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this._medicalRecords != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.MedicalRecords)));
        }
    }
}
