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
            this.Doctor = doctor;
            this.Patient = patient;
            this.DateTime = DateTime.Now;
        }

        public Appointment(Doctor doctor, Patient patient, DateTime dateTime)
        {
            this.Doctor = doctor;
            this.Patient = patient;
            this.DateTime = dateTime;
        }


        public void Copy(Appointment newAppointment)
        {
            this.Doctor = newAppointment.Doctor;
            this.Patient = newAppointment.Patient;
            this.DateTime = newAppointment.DateTime;
        }

        public bool Equals([AllowNull] Appointment other)
        {
            if (other == null)
                return false;

            return this.Id == other.Id;
        }
    }
}
