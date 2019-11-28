using System;
using System.ComponentModel.DataAnnotations;


namespace Coursework.Types
{
    public class MedicalRecord
    {
        [Key]
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public DateTime From { get; set; }
        public DateTime Until { get; set; }
        public string Diagnosis { get; set; }

        public MedicalRecord()
        {
        }

        public MedicalRecord(Patient patient, DateTime from, DateTime until, string diagnosis)
        {
            Patient = patient;
            From = from;
            Until = until;
            Diagnosis = diagnosis;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(MedicalRecord))
                return false;

            return this.Id == ((MedicalRecord)obj).Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
