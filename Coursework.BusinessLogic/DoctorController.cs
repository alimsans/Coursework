using Coursework.DAL;
using Coursework.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Coursework.BLL
{
    public class DoctorController : Controller
    {
        public DoctorController()
        {
        }

        public DoctorController(DbContextOptions<HospitalContext> options)
            : base(options)
        {
        }


        /// <summary>
        /// Adds an instance of a doctor in the db
        /// </summary>
        /// <param name="firstName">Doctor's first name</param>
        /// <param name="lastName">Doctor's last name</param>
        /// <param name="occupation">Doctor's occupation</param>\
        /// <exception cref="ArgumentNullException">Doctor is null</exception>
        public void AddDoctor(Doctor doctor)
        {
            if (doctor == null) throw new ArgumentNullException(nameof(doctor));

            this._context.Doctors.Add(doctor);
            this._context.SaveChanges();
        }

        /// <summary>
        /// Gets an instance of Doctor by id from the db
        /// </summary>
        /// <param name="id">Doctor id</param>
        /// <returns>Doctor from the db. NULL if not found</returns>
        public Doctor GetDoctor(int id)
        {
            return this._context.Doctors.AsNoTracking().Where(d => d.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Gets all doctors from the db
        /// </summary>
        /// <returns>Doctors from the db. NULL if not found.</returns>
        public ICollection<Doctor> GetDoctors()
        {
            return this._context.Doctors.AsNoTracking().ToList();
        }

        /// <summary>
        /// Gets all doctors by their first and last name
        /// </summary>
        /// <param name="firstName">first name</param>
        /// <param name="lastName">last name</param>
        /// <returns>Doctors from the db. NULL if not found.</returns>
        public ICollection<Doctor> GetDoctorsByName(string firstName, string lastName)
        {
            return this._context.Doctors.AsNoTracking().Where(d => d.FirstName == firstName && d.LastName == lastName).ToList();
        }

        /// <summary>
        /// Gets all doctors with specified occupation
        /// </summary>
        /// <param name="occupation">doctors' occupation</param>
        /// <returns>Doctors from the db. NULL if no doctor with specified occupation.</returns>
        public ICollection<Doctor> GetDoctorsByOccupation(string occupation)
        {
            return this._context.Doctors.AsNoTracking().Where(d => d.Occupation == occupation).ToList();
        }

        /// <summary>
        /// Removes a doctor from the db
        /// </summary>
        /// <param name="id">doctor's id</param>
        /// <exception cref="DbUpdateConcurrencyException">Doctor was not found</exception>
        public void RemoveDoctor(Doctor doctor)
        {
            this._context.Doctors.Remove(doctor);
            this._context.SaveChanges();
        }

        /// <summary>
        /// Alters doctor's info in the db
        /// </summary>
        /// <param name="id">Id of the Doctor to alter</param>
        /// <param name="newDoctor">New Doctor</param>
        /// <exception cref="ArgumentNullException">Doctor is null</exception>
        /// <exception cref="KeyNotFoundException">Invalid doctor id</exception>
        public void AlterDoctorInfo(Doctor oldDoctor, Doctor newDoctor)
        {
            if (newDoctor == null) throw new ArgumentNullException(nameof(newDoctor));
            if (oldDoctor == null) throw new ArgumentNullException(nameof(oldDoctor));

            Doctor.Clone(ref oldDoctor, newDoctor);
            this._context.Doctors.Update(oldDoctor);
            this._context.SaveChanges();
        }
    }
}
