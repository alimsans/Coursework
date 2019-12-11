﻿using Coursework.DAL;
using Coursework.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coursework.BLL
{
    /// <summary>
    /// Class object should be disposed with every use.
    /// </summary>
    public class MedicalRecordsController : Controller
    {
        public MedicalRecordsController()
        {
        }

        public MedicalRecordsController(DbContextOptions<HospitalContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Adds a medical record to the db
        /// </summary>
        /// <param name="record">record to be added</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddMedicalRecord(MedicalRecord record)
        {
            if (record == null) throw new ArgumentNullException(nameof(record));
            if (record.Patient == null) throw new ArgumentNullException(nameof(record.Patient));

            Patient tmpPatient = record.Patient;
            record.Patient = null;
            _context.MedicalRecords.Add(record).Entity.Patient = tmpPatient;
            _context.SaveChanges();
        }

        /// <summary>
        /// Removes a medical record from the db
        /// </summary>
        /// <param name="record">record to be removed</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void RemoveMedicalRecord(MedicalRecord record)
        {
            if (record == null) throw new ArgumentNullException(nameof(record));

            _context.MedicalRecords.Remove(record);
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets all patient's medical records
        /// </summary>
        /// <param name="patient">Patient whose records to retrieve</param>
        /// <returns>Collection of medical records</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ICollection<MedicalRecord> GetPatientsMedicalRecords(Patient patient)
        {
            if (patient == null) throw new ArgumentNullException(nameof(patient));

            return _context.MedicalRecords
                .Include(p => p.Patient)
                .AsNoTracking()
                .Where(m => m.Patient == patient)
                .ToList();
        }

        /// <summary>
        /// Gets medical record by id
        /// </summary>
        /// <param name="id">Id of the record</param>
        /// <returns>Medical record</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public MedicalRecord GetMedicalRecord(int id)
        {
            if (id < 0) throw new ArgumentOutOfRangeException(nameof(id));

            return _context.MedicalRecords
                .Include(p => p.Patient)
                .AsNoTracking()
                .Where(m => m.Id == id)
                .FirstOrDefault();
        }
    }
}
