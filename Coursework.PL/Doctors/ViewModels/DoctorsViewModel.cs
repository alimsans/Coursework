using Coursework.BLL;
using Coursework.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.PL.Doctors.ViewModels
{
    internal class DoctorsViewModel : INotifyPropertyChanged
    {
        private DoctorController _controller;
        private ObservableCollection<Doctor> _doctors;


        internal ObservableCollection<Doctor> Doctors { get => _doctors; }

        internal Doctor SelectedDoctor { get; set; }

        internal DoctorsViewModel()
        {
            _doctors = new ObservableCollection<Doctor>();
            _doctors.CollectionChanged += Doctors_CollectionChanged;
        }

        internal async Task UpdateDoctorsAsync()
        {
            List<Doctor> doctors = null;
            using (_controller = new DoctorController())
                await Task.Run(() => doctors = (List<Doctor>)_controller.GetDoctors());

            if (doctors != null)
            {
                lock (_doctors)
                {
                    _doctors.Clear();
                    foreach (var doctor in doctors)
                    {
                        _doctors.Add(doctor);
                    }
                }
            }
        }

        internal async Task AddDoctorAsync(Doctor doctor)
        {
            using (_controller = new DoctorController())
                await Task.Run(() => _controller.AddDoctor(doctor));

            await this.UpdateDoctorsAsync();
        }

        internal async Task EditDoctorAsync(Doctor oldPatient, Doctor newDoctor)
        {
            using (_controller = new DoctorController())
                await Task.Run(() => _controller.AlterDoctorInfo(SelectedDoctor, newDoctor));

            await this.UpdateDoctorsAsync();
        }

        internal async Task RemoveDoctorAsync(Doctor doctor)
        {
            using (_controller = new DoctorController())
                await Task.Run(() => _controller.RemoveDoctor(doctor));

            await this.UpdateDoctorsAsync();
        }

        internal async Task SearchDoctorsByNameAsync(string firstName, string lastName)
        {
            List<Doctor> searchResult = null;
            using (_controller = new DoctorController())
                await Task.Run(() => searchResult = (List<Doctor>)_controller.GetDoctorsByName(firstName, lastName));

            if (searchResult != null)
            {
                lock (_doctors)
                {
                    _doctors.Clear();

                    foreach (Doctor doctor in searchResult)
                    {
                        _doctors.Add(doctor);
                    }
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void Doctors_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_doctors != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Doctors)));
        }
    }
}
