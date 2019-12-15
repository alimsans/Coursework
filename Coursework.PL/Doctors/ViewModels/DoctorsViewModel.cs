using Coursework.BLL;
using Coursework.Types;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Coursework.PL.Doctors.ViewModels
{
    internal class DoctorsViewModel : INotifyPropertyChanged
    {
        private DoctorController _controller;
        private ObservableCollection<Doctor> _doctors;

        internal ObservableCollection<Doctor> Doctors { get => this._doctors; }
        internal Doctor SelectedDoctor { get; set; }

        internal DoctorsViewModel()
        {
            this._doctors = new ObservableCollection<Doctor>();
            this._doctors.CollectionChanged += this.Doctors_CollectionChanged;
        }

        internal async Task UpdateDoctorsAsync()
        {
            List<Doctor> doctors = null;
            using (this._controller = new DoctorController())
                await Task.Run(() => doctors = (List<Doctor>)this._controller.GetDoctors());

            if (doctors != null)
            {
                lock (this._doctors)
                {
                    this._doctors.Clear();
                    foreach (var doctor in doctors)
                    {
                        this._doctors.Add(doctor);
                    }
                }
            }
        }

        internal async Task AddDoctorAsync(Doctor doctor)
        {
            using (this._controller = new DoctorController())
                await Task.Run(() => this._controller.AddDoctor(doctor));

            await this.UpdateDoctorsAsync();
        }

        internal async Task EditDoctorAsync(Doctor oldPatient, Doctor newDoctor)
        {
            using (this._controller = new DoctorController())
                await Task.Run(() => this._controller.AlterDoctorInfo(this.SelectedDoctor, newDoctor));

            await this.UpdateDoctorsAsync();
        }

        internal async Task RemoveDoctorAsync(Doctor doctor)
        {
            using (this._controller = new DoctorController())
                await Task.Run(() => this._controller.RemoveDoctor(doctor));

            await this.UpdateDoctorsAsync();
        }

        internal async Task SearchDoctorsByNameAsync(string firstName, string lastName)
        {
            List<Doctor> searchResult = null;
            using (this._controller = new DoctorController())
                await Task.Run(() => searchResult = (List<Doctor>)this._controller.GetDoctorsByName(firstName, lastName));

            if (searchResult != null)
            {
                lock (this._doctors)
                {
                    this._doctors.Clear();

                    foreach (Doctor doctor in searchResult)
                    {
                        this._doctors.Add(doctor);
                    }
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void Doctors_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this._doctors != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Doctors)));
        }
    }
}
