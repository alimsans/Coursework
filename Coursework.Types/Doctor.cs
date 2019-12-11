using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Coursework.Types
{
    public class Doctor : IEquatable<Doctor>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Occupation { get; set; }
        public List<Appointment> Appointments { get; set; }
        public ICollection<WorkDay> WorkDays { get; set; }

        public Doctor()
        {
        }


        public Doctor(string firstName, string lastName, string occupation)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Occupation = occupation;
            this.WorkDays = new WorkDay().GetDefaultWorkWeek();
        }

        public Doctor(string firstName, string lastName, string occupation, ICollection<WorkDay> workDays)
        {
            this.FirstName  = firstName;
            this.LastName   = lastName;
            this.Occupation = occupation;
            this.WorkDays   = workDays;
        }

        public void Copy(Doctor other)
        {
            this.FirstName  = other.FirstName;
            this.LastName   = other.LastName;
            this.Occupation = other.Occupation;
        }

        public bool Equals([AllowNull] Doctor other)
        {
            if (other == null || other.GetType() != typeof(Doctor))
                return false;

            return this.Id == other.Id;
        }
    }
}
