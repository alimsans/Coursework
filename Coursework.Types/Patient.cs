using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Coursework.Types
{
    public class Patient : IEquatable<Patient>
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<MedicalRecord> MedicalRecords { get; set; }

        public Patient()
        {
        }

        public Patient(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void Copy(Patient other)
        {
            FirstName = other.FirstName;
            LastName = other.LastName;
        }

        public bool Equals([AllowNull] Patient other)
        {
            if (other == null)
                return false;

            return Id == other.Id;
        }
    }
}
