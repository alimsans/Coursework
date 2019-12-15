using Coursework.Types;
using System.Collections.Generic;

namespace Coursework.PL.Appointments.Models
{
    internal class AppointmentModel
    {
        public int AppointmentId { get; private set; }
        public string DateTime { get; private set; }
        public int DoctorId { get; private set; }
        public string DoctorFirstName { get; private set; }
        public string DoctorLastName { get; private set; }
        public string DoctorOccupation { get; private set; }
        public int PatientId { get; private set; }
        public string PatientFirstName { get; private set; }
        public string PatientLastName { get; private set; }

        internal AppointmentModel()
        {
        }

        internal AppointmentModel(Appointment appointment)
        {
            //Appointment = appointment;
            AppointmentId = appointment.Id;
            DateTime = appointment.DateTime.ToString("yyyy-MM-dd HH:MM");

            DoctorId = appointment.Doctor.Id;
            DoctorFirstName = appointment.Doctor.FirstName;
            DoctorLastName = appointment.Doctor.LastName;
            DoctorOccupation = appointment.Doctor.Occupation;

            PatientId = appointment.Patient.Id;
            PatientFirstName = appointment.Patient.FirstName;
            PatientLastName = appointment.Patient.LastName;
        }

        internal static ICollection<AppointmentModel> GetAppointmentModels(ICollection<Appointment> appointments)
        {
            ICollection<AppointmentModel> appointmentModels = new List<AppointmentModel>();

            foreach (var appointment in appointments)
            {
                appointmentModels.Add(new AppointmentModel(appointment));
            }

            return appointmentModels;
        }
    }
}
