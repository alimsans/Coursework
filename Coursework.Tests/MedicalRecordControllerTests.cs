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
            _options = new DbContextOptionsBuilder<HospitalContext>()
                .UseInMemoryDatabase(databaseName: "HospitalTestDb" + random.Next(int.MinValue, int.MaxValue))
                .Options;

            _controller = new MedicalRecordsController(_options);
        }

        public void Dispose()
        {
            _controller.DropDatabase();
            _controller.Dispose();
        }

        [Fact]
        public void AddMedicalRecord_ShouldAdd()
        {
            Patient patient = new Patient("Foo", "Numb");
            MedicalRecord expected = new MedicalRecord(patient, DateTime.Parse("2019-01-01"), DateTime.Now, "Cancer");

            _controller.AddMedicalRecord(expected);

            var actual = (List<MedicalRecord>)_controller.GetPatientsMedicalRecords(patient);

            Assert.Contains(expected, actual);
        }

        [Fact]
        public void AddMedicalRecord_ShouldThrowArgNull()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.AddMedicalRecord(null));
        }

        [Fact]
        public void RemoveMedicalRecord_ShouldRemove()
        {
            Patient patient = new Patient("Foo", "To record");
            MedicalRecord recordToRemove = new MedicalRecord
                (patient, DateTime.Parse("2019-10-10"), DateTime.Parse("2019-12-12"), "Cancer");

            _controller.AddMedicalRecord(recordToRemove);
            Assert.Equal(recordToRemove, _controller.GetMedicalRecord(recordToRemove.Id));

            _controller.RemoveMedicalRecord(recordToRemove);
            Assert.Null(_controller.GetMedicalRecord(recordToRemove.Id));
        }
    }
}
