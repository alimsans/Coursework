using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Coursework.BLL;
using Coursework.Types;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Coursework.PL.ViewModels
{
    internal class PatientsViewModel : INotifyPropertyChanged
    {
        private PatientController _controller;
        private ObservableCollection<Patient> _patients;
        internal ObservableCollection<Patient> Patients { get => _patients; }

        public Patient SelectedPatient { get; set; }

        internal PatientsViewModel()
        {
            _patients = new ObservableCollection<Patient>();
            _patients.CollectionChanged += Patients_CollectionChanged;
        }


        /// <summary>
        /// Asynchronously update Patients collection.
        /// </summary>
        internal async Task UpdatePatientsAsync()
        {
            List<Patient> patients = null;
            await Task.Run(() => patients = (List<Patient>)_controller.GetPatients());

            if (patients != null)
            {
                lock (_patients)
                {
                    _patients.Clear();
                    foreach (var patient in patients)
                    {
                        _patients.Add(patient);
                    }
                }
            }

            patients.Clear();
        }

        /// <summary>
        /// Asynchronously add a patient
        /// </summary>
        internal async Task AddPatientAsync(Patient patient)
        {
            await Task.Run(() => _controller.AddPatient(patient));

            await this.UpdatePatientsAsync();
        }

        /// <summary>
        /// Asynchronously edits a patient
        /// </summary>
        /// <param name="oldPatient">old patient to be edited</param>
        /// <param name="newPatient">new patient to replace the old one</param>
        internal async Task EditPatientAsync(Patient oldPatient, Patient newPatient)
        {
            using (_controller = new PatientController())
            {
                await Task.Run(() => _controller.AlterPatientInfo(SelectedPatient, newPatient));

                await this.UpdatePatientsAsync();
            }
        }

        /// <summary>
        /// Asynchronously removes patient
        /// </summary>
        /// <param name="patient">patient to be removed</param>
        internal async Task RemovePatientAsync(Patient patient)
        {
            await Task.Run(() => _controller.RemovePatient(patient));

            await this.UpdatePatientsAsync();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void Patients_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(_patients != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Patients)));
        }
    }
}
