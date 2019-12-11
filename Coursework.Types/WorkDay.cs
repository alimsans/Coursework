using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace Coursework.Types
{
    public enum Day { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday }
    public class WorkDay
    {
        [Key]
        public int Id { get; set; }
        public Day Day { get; set; }
        public DateTime From { get; set; }
        public DateTime Until { get; set; }
        public Doctor Doctor { get; set; }

        public WorkDay()
        {
        }
            
        public WorkDay(Day day, DateTime from, DateTime until)
        {
            this.Day   = day;
            this.From  = from;
            this.Until = until;
        }

        public WorkDay GetDefaultWorkDay(Day day)
        {
            CultureInfo format = new CultureInfo("ru-RU"); 
            return new WorkDay
                (day, DateTime.ParseExact("08:00:00", "HH:mm:ss", format ), DateTime.ParseExact("16:00:00", "HH:mm:ss", format));
        }

        public ICollection<WorkDay> GetDefaultWorkWeek()
        {
            ICollection<Day> days = new List<Day>(7) 
                { Day.Monday, Day.Tuesday, Day.Wednesday, Day.Thursday, Day.Friday, Day.Saturday, Day.Sunday };

            ICollection<WorkDay> workDays = new List<WorkDay>(7); 

            foreach (Day day in days)
            {
                workDays.Add(this.GetDefaultWorkDay(day));
            }

            return workDays;
        }
    }
}
