using Coursework.BLL;
using Coursework.DAL;
using Coursework.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;

namespace Coursework.Tests
{
    public class MedicalRecordControllerTests : IDisposable
    {
        private DbContextOptions<HospitalContext> _options;
        private MedicalRecordsController _controller;

        public MedicalRecordControllerTests()
        {
            Random random = new Random();
            this._options = new DbContextOptionsBuilder<HospitalContext>()
                .UseInMemoryDatabase(databaseName: "HospitalTestDb" + random.Next(int.MinValue, int.MaxValue))
                .Options;

            this._controller = new MedicalRecordsController(this._options);
        }

        public void Dispose()
        {
            this._controller.DropDatabase();
            this._controller.Dispose();
        }

        [Fact]
        public void AddMedicalRecord_ShouldAdd()
        {
            Patient patient = new Patient("Foo", "Numb");
            MedicalRecord expected = new MedicalRecord(patient, DateTime.Parse("2019-01-01"), DateTime.Now, "Cancer");

            this._controller.AddMedicalRecord(expected);

            var actual = (List<MedicalRecord>)this._controller.GetPatientsMedicalRecords(patient);

            Assert.Contains(expected, actual);
        }

        [Fact]
        public void AddMedicalRecord_ShouldThrowArgNull()
        {
            Assert.Throws<ArgumentNullException>(() => this._controller.AddMedicalRecord(null));
        }

        [Fact]
        public void RemoveMedicalRecord_ShouldRemove()
        {
            Patient patient = new Patient("Foo", "To record");
            MedicalRecord recordToRemove = new MedicalRecord
                (patient, DateTime.Parse("2019-10-10"), DateTime.Parse("2019-12-12"), "Cancer");

            this._controller.AddMedicalRecord(recordToRemove);
            Assert.Equal(recordToRemove, this._controller.GetMedicalRecord(recordToRemove.Id));

            this._controller.RemoveMedicalRecord(recordToRemove);
            Assert.Null(this._controller.GetMedicalRecord(recordToRemove.Id));
        }
    }
}
