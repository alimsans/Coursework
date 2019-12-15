using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Coursework.Types
{
    public class Patient : IEquatable<Patient>
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

        public void Copy(Patient other)
        {
            this.FirstName = other.FirstName;
            this.LastName = other.LastName;
        }

        public bool Equals([AllowNull] Patient other)
        {
            if (other == null)
                return false;

            return this.Id == other.Id;
        }
    }
}
