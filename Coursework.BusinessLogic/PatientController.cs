﻿using Coursework.DAL;
using Coursework.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Coursework.BLL
{
    /// <summary>
    /// Class object should be disposed with every use.
    /// </summary>
    public class PatientController : Controller
    {
        public PatientController()
        {
        }

        public PatientController(DbContextOptions<HospitalContext> options)
            : base(options)
        {
        }


        /// <summary>
        /// Adds an instance of a patient in the db
        /// </summary>
        /// <param name="firstName">Patient's first name</param>
        /// <param name="lastName">Patient's last name</param>
        /// <param name="occupation">Patient's occupation</param>
        /// <returns>Id of the added patient</returns>
        public int AddPatient(Patient patient)
        {
            int id = _context.Patients.Add(patient).Entity.Id;
            _context.SaveChanges();

            return id;
        }

        /// <summary>
        /// Gets an instance of Patient by id from the db
        /// </summary>
        /// <param name="id">Patient id</param>
        /// <returns>Patient from the db. NULL if not found</returns>
        public Patient GetPatient(int id)
        {
            return _context.Patients
                .Include(p => p.Appointments)
                .Include(r => r.MedicalRecords)
                .AsNoTracking()
                .Where(d => d.Id == id)
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets all patients from the db
        /// </summary>
        /// <returns>Patients from the db. NULL if not found.</returns>
        public ICollection<Patient> GetPatients()
        {
            return _context.Patients
                .Include(p => p.Appointments)
                .Include(r => r.MedicalRecords)
                .AsNoTracking()
                .ToList();
        }

        /// <summary>
        /// Gets all patients by their first and last name
        /// </summary>
        /// <param name="firstName">first name</param>
        /// <param name="lastName">last name</param>
        /// <returns>Patients from the db. NULL if not found.</returns>
        public ICollection<Patient> GetPatientsByName(string firstName, string lastName)
        {
            return _context.Patients
                .Include(p => p.Appointments)
                .Include(r => r.MedicalRecords)
                .AsNoTracking()
                .Where(d => d.FirstName == firstName && d.LastName == lastName)
                .ToList();
        }

        /// <summary>
        /// Removes a patient from the db
        /// </summary>
        /// <param name="id">patient's id</param>
        /// <exception cref="DbUpdateConcurrencyException">Patient was not found</exception>
        public void RemovePatient(Patient patient)
        {
            _context.Remove(patient);
            _context.SaveChanges();
        }

        /// <summary>
        /// Alters patient's info in the db
        /// </summary>
        /// <param name="id">Id of the Patient to alter</param>
        /// <param name="newPatient">New Patient</param>
        /// <exception cref="ArgumentException">Patient is null</exception>
        /// <exception cref="KeyNotFoundException">Invalid patient id</exception>
        public void AlterPatientInfo(Patient oldPatient, Patient newPatient)
        {
            if (newPatient == null) throw new ArgumentNullException(nameof(newPatient));
            if (oldPatient == null) throw new ArgumentNullException(nameof(oldPatient));

            oldPatient.Copy(newPatient);
            _context.Patients.Update(oldPatient);
            _context.SaveChanges();
        }
    }
}
