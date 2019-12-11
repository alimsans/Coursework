using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;

using Coursework.Types;
using Coursework.BLL;
using System.Threading.Tasks;

namespace Coursework.PL.ViewModels
{
    internal class MedicalRecordsViewModel : INotifyPropertyChanged
    {
        private MedicalRecordsController _controller;
        private ObservableCollection<MedicalRecord> _medicalRecords;
        internal ObservableCollection<MedicalRecord> MedicalRecords { get => _medicalRecords; }

        internal Patient SelectedPatient { get; set; }
        internal MedicalRecord SelectedMedicalRecord { get; set; }

        internal MedicalRecordsViewModel(Patient patient)
        {
            SelectedPatient = patient;
            _medicalRecords = new ObservableCollection<MedicalRecord>();
            _medicalRecords.CollectionChanged += MedicalRecords_CollectionChanged;
        }

        /// <summary>
        /// Asynchronously update Medical Records collection.
        /// </summary>
        internal async Task UpdateMedicalRecordsAsync()
        {
            List<MedicalRecord> medicalRecords = null;
            using (_controller = new MedicalRecordsController())
                await Task.Run(
                    () => medicalRecords = (List<MedicalRecord>)_controller.GetPatientsMedicalRecords(SelectedPatient));

            if (medicalRecords != null)
            {
                lock (_medicalRecords)
                {
                    _medicalRecords.Clear();
                    foreach (var record in medicalRecords)
                    {
                        _medicalRecords.Add(record);
                    }
                }
            }
        }

        /// <summary>
        /// Asynchronously add a medical record
        /// </summary>
        internal async Task AddMedicalRecordAsync(MedicalRecord record)
        {
            using (_controller = new MedicalRecordsController())
                await Task.Run(() => _controller.AddMedicalRecord(record));

            await this.UpdateMedicalRecordsAsync();
        }

        /// <summary>
        /// Asynchronously removes a medical record
        /// </summary>
        internal async Task RemoveMedicalRecordAsync(MedicalRecord record)
        {
            using (_controller = new MedicalRecordsController())
                await Task.Run(() => _controller.RemoveMedicalRecord(record));

            await this.UpdateMedicalRecordsAsync();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void MedicalRecords_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_medicalRecords != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MedicalRecords)));
        }
    }
}
