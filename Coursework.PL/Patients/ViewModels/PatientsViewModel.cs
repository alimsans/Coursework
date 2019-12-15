using Coursework.BLL;
using Coursework.Types;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Coursework.PL.ViewModels
{
    internal class PatientsViewModel : INotifyPropertyChanged
    {
        private PatientController _controller;
        private ObservableCollection<Patient> _patients;
        internal ObservableCollection<Patient> Patients { get => this._patients; }

        internal Patient SelectedPatient { get; set; }

        internal PatientsViewModel()
        {
            this._patients = new ObservableCollection<Patient>();
            this._patients.CollectionChanged += this.Patients_CollectionChanged;
        }


        /// <summary>
        /// Asynchronously update Patients collection.
        /// </summary>
        internal async Task UpdatePatientsAsync()
        {
            List<Patient> patients = null;
            using (this._controller = new PatientController())
                await Task.Run(() => patients = (List<Patient>)this._controller.GetPatients());

            if (patients != null)
            {
                lock (this._patients)
                {
                    this._patients.Clear();
                    foreach (var patient in patients)
                    {
                        this._patients.Add(patient);
                    }
                }
            }
        }

        /// <summary>
        /// Asynchronously add a patient
        /// </summary>
        internal async Task AddPatientAsync(Patient patient)
        {
            using (this._controller = new PatientController())
                await Task.Run(() => this._controller.AddPatient(patient));

            await this.UpdatePatientsAsync();
        }

        /// <summary>
        /// Asynchronously edits a patient
        /// </summary>
        /// <param name="oldPatient">old patient to be edited</param>
        /// <param name="newPatient">new patient to replace the old one</param>
        internal async Task EditPatientAsync(Patient oldPatient, Patient newPatient)
        {
            using (this._controller = new PatientController())
                await Task.Run(() => this._controller.AlterPatientInfo(this.SelectedPatient, newPatient));

            await this.UpdatePatientsAsync();
        }

        /// <summary>
        /// Asynchronously removes patient
        /// </summary>
        /// <param name="patient">patient to be removed</param>
        internal async Task RemovePatientAsync(Patient patient)
        {
            using (this._controller = new PatientController())
                await Task.Run(() => this._controller.RemovePatient(patient));

            await this.UpdatePatientsAsync();
        }

        internal async Task SearchPatientsByNameAsync(string firstName, string secondName)
        {
            List<Patient> searchResult = null;
            using (this._controller = new PatientController())
                await Task.Run(() => searchResult = (List<Patient>)this._controller.GetPatientsByName(firstName, secondName));

            if (searchResult != null)
            {
                lock (this._patients)
                {
                    this._patients.Clear();

                    foreach (Patient patient in searchResult)
                    {
                        this._patients.Add(patient);
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void Patients_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this._patients != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Patients)));
        }
    }
}
