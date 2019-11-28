using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Coursework.Types
{
    public class Doctor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Occupation { get; set; }
        public List<Appointment> Appointments { get; set; }

        public Doctor()
        {
        }


        public Doctor(string firstName, string lastName, string occupation)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Occupation = occupation;
        }

        public static void Clone(ref Doctor oldDoctor, Doctor newDoctor)
        {
            oldDoctor.FirstName = newDoctor.FirstName;
            oldDoctor.LastName = newDoctor.LastName;
            oldDoctor.Occupation = newDoctor.Occupation;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Doctor))
                return false;

            return this.Id == ((Doctor)obj).Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
