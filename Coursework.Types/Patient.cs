using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Coursework.Types
{
    public class Patient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<MedicalRecord> MedicalRecords { get; set; }

        public Patient()
        {
        }

        public Patient(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public static void Clone(ref Patient oldDoctor, Patient newDoctor)
        {
            oldDoctor.FirstName = newDoctor.FirstName;
            oldDoctor.LastName = newDoctor.LastName;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Patient))
                return false;

            return this.Id == ((Patient)obj).Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
