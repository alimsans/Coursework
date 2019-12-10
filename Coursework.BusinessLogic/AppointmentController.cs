using Coursework.DAL;
using Coursework.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Coursework.BLL
{
    public class AppointmentController : Controller
    {
        public AppointmentController()
        {
        }

        public AppointmentController(DbContextOptions<HospitalContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Add an appointment to the db
        /// </summary>
        /// <exception cref="ArgumentNullException">Appointment, Appointment.Doctor or Appointment.Patient is NULL</exception>
        public void AddAppointment(Appointment appointment)
        {
            if (appointment == null) throw new ArgumentNullException(nameof(appointment));
            if (appointment.Doctor == null) throw new ArgumentNullException(nameof(appointment.Doctor));
            if (appointment.Patient == null) throw new ArgumentNullException(nameof(appointment.Patient));

            this._context.Appointments.Add(appointment);
            this._context.SaveChanges();
        }

        public void RemoveAppointment(Appointment appointment)
        {
            this._context.Appointments.Remove(appointment);
            this._context.SaveChanges();
        }

        /// <summary>
        /// Gets an instance of Doctor by id from the db
        /// </summary>
        /// <param name="id">Appointment id</param>
        public Appointment GetAppointment(int id)
        {
            return this._context.Appointments.AsNoTracking().Where(d => d.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Alters an existing appointment in the db
        /// </summary>
        /// <param name="id">Id of the appointment to be altered</param>
        /// <param name="appointment">New appointment to be replaced with</param>
        /// <exception cref="ArgumentNullException">Appointment, Appointment.Doctor or Appointment.Patient is null</exception>
        /// <exception cref="KeyNotFoundException">Invalid appointment id</exception>
        public void AlterAppointment(Appointment oldAppointment, Appointment newAppointment)
        {
            if (newAppointment  == null) throw new ArgumentNullException(nameof(newAppointment));
            if (newAppointment.Doctor == null) throw new ArgumentNullException(nameof(newAppointment.Doctor));
            if (newAppointment.Patient == null) throw new ArgumentNullException(nameof(newAppointment.Patient));
            if (oldAppointment == null) throw new ArgumentNullException(nameof(oldAppointment));

            Appointment.Сlone(ref oldAppointment, newAppointment);
            this._context.Appointments.Update(oldAppointment);
            this._context.SaveChanges();
        }

        /// <summary>
        /// Gets all appointments
        /// </summary>
        /// <returns>Appointments in the db. NULL if no appointments found</returns>
        public ICollection<Appointment> GetAppointments()
        {
            return this._context.Appointments.ToList();
        }

        /// <summary>
        /// Gets appointments in the specified range of dates (inclusive)
        /// </summary>
        /// <returns>Matched appointments. NULL if no appointments found.</returns>
        /// <exception cref="ArgumentException">from >= until</exception>
        public ICollection<Appointment> GetAppointments(DateTime from, DateTime until)
        {
            if (from >= until) throw new ArgumentException("Invalid date time");

            return this._context.Appointments.Where(a => a.DateTime >= from && a.DateTime <= until).ToList();
        }

        /// <summary>
        /// Gets appointments in the specified day
        /// </summary>
        /// <param name="day">Day of appointment</param>
        /// <returns>Matched appointments. NULL if no appointments found.</returns>
        public ICollection<Appointment> GetAppointments(DateTime day)
        {
            return this._context.Appointments.Where(a => a.DateTime.Date == day.Date).ToList();
        }


        /// <summary>
        /// Gets all doctor's appointments
        /// </summary>
        /// <param name="doctor">Doctore whose appointments to find</param>
        /// <returns>Matched appointments. NULL if no appointments found.</returns>
        /// <exception cref="ArgumentNullException">Doctor is null</exception>
        public ICollection<Appointment> GetAppointments(Doctor doctor)
        {
            if (doctor == null) throw new ArgumentNullException(nameof(doctor));

            return this._context.Appointments.Where(a => a.Doctor.Id == doctor.Id).ToList();
        }
    }
}
