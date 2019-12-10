using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace Coursework.Types
{
    public class Appointment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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


        public static void Сlone(ref Appointment oldAppointment, Appointment newAppointment)
        {
            oldAppointment.Doctor = newAppointment.Doctor;
            oldAppointment.Patient = newAppointment.Patient;
            oldAppointment.DateTime = newAppointment.DateTime;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Appointment))
                return false;

            return this.Id == ((Appointment)obj).Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
