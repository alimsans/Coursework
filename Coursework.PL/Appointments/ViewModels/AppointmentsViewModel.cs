using Coursework.BLL;
using Coursework.PL.Appointments.Models;
using Coursework.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Coursework.PL.Appointments.ViewModels
{
    internal class AppointmentsViewModel : INotifyPropertyChanged
    {
        private AppointmentController _controller;
        private ObservableCollection<Appointment> _appointments { get; }

        internal ObservableCollection<AppointmentModel> AppointmentModels { get; private set; }
        internal AppointmentModel SelectedAppointment { get; set; }

        internal AppointmentsViewModel()
        {
            this._appointments = new ObservableCollection<Appointment>();
            this.AppointmentModels = new ObservableCollection<AppointmentModel>();
            this.AppointmentModels.CollectionChanged += this.AppointmentModels_CollectionChanged;
        }

        internal async Task UpdateAppointmentsAsync()
        {
            List<Appointment> appointments = null;
            using (this._controller = new AppointmentController())
                await Task.Run(() => appointments = (List<Appointment>)this._controller.GetAppointments());

            if (appointments != null)
            {
                lock (this._appointments)
                lock (this.AppointmentModels)
                {
                    this._appointments.Clear();
                    this.AppointmentModels.Clear();
                    foreach (var appointment in appointments)
                    {
                        this._appointments.Add(appointment);
                        this.AppointmentModels.Add(new AppointmentModel(appointment));
                    }

                }
            }
        }

        internal async Task AddAppointmentAsync(int doctorId, int patientId, DateTime dateTime)
        {
            Doctor doctor = null;
            Patient patient = null;
            
            using (var doctorController = new DoctorController())
                await Task.Run(() => doctor = doctorController.GetDoctor(doctorId));
            if (doctor == null)
                throw new KeyNotFoundException("Doctor with the specified id doesn't exist.");

            using (var patientController = new PatientController())
                await Task.Run(() => patient = patientController.GetPatient(patientId));
            if (patient == null)
                throw new KeyNotFoundException("Patient with the specified id doesn't exist.");
            

            Appointment appointment = new Appointment(doctor, patient, dateTime);
            using (_controller = new AppointmentController())
               await Task.Run(() => _controller.AddAppointment(appointment));
            
            await this.UpdateAppointmentsAsync();
        }

        internal async Task RemoveAppointmentAsync(AppointmentModel appointmentModel)
        {
            Appointment appointment = null;
            using (this._controller = new AppointmentController())
            {
                await Task.Run(() => appointment = this._controller.GetAppointment(appointmentModel.AppointmentId));
                await Task.Run(() => this._controller.RemoveAppointment(appointment));
            }

            await this.UpdateAppointmentsAsync();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void AppointmentModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (AppointmentModels != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AppointmentModels)));
        }
    }
}
