using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Coursework.Types
{
    public class Doctor : IEquatable<Doctor>
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Occupation { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<WorkDay> WorkDays { get; set; }

        public Doctor()
        {
        }


        public Doctor(string firstName, string lastName, string occupation)
        {
            FirstName = firstName;
            LastName = lastName;
            Occupation = occupation;
            WorkDays = new WorkDay().GetDefaultWorkWeek();
        }

        public Doctor(string firstName, string lastName, string occupation, ICollection<WorkDay> workDays)
        {
            FirstName = firstName;
            LastName = lastName;
            Occupation = occupation;
            WorkDays = workDays;
        }

        public void Copy(Doctor other)
        {
            FirstName = other.FirstName;
            LastName = other.LastName;
            Occupation = other.Occupation;
        }

        public bool Equals([AllowNull] Doctor other)
        {
            if (other == null)
                return false;

            return Id == other.Id;
        }
    }
}
