using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace Coursework.Types
{
    public class Appointment : IEquatable<Appointment>
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public DateTime DateTime { get; set; }

        public Appointment()
        {
        }

        public Appointment(Doctor doctor, Patient patient)
        {
            Doctor = doctor;
            Patient = patient;
            DateTime = DateTime.Now;
        }

        public Appointment(Doctor doctor, Patient patient, DateTime dateTime)
        {
            Doctor = doctor;
            Patient = patient;
            DateTime = dateTime;
        }


        public void Copy(Appointment newAppointment)
        {
            Doctor = newAppointment.Doctor;
            Patient = newAppointment.Patient;
            DateTime = newAppointment.DateTime;
        }

        public bool Equals([AllowNull] Appointment other)
        {
            if (other == null)
                return false;

            return Id == other.Id;
        }
    }
}
