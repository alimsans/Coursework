using Coursework.BLL;
using Coursework.DAL;
using Coursework.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Coursework.Tests
{
    public class MedicalRecordControllerTests : IDisposable
    {
        private DbContextOptions<HospitalContext> _options;
        private MedicalRecordController _controller;

        public MedicalRecordControllerTests()
        {
            Random random = new Random();
            this._options = new DbContextOptionsBuilder<HospitalContext>()
                .UseInMemoryDatabase(databaseName: "HospitalTestDb" + random.Next(int.MinValue, int.MaxValue))
                .Options;

            this._controller = new MedicalRecordController(this._options);
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

            _controller.AddMedicalRecord(expected);

            var actual = (List<MedicalRecord>)_controller.GetPatientsMedicalRecords(patient);

            Assert.Contains(expected, actual);
        }

        [Fact]
        public void AddMedicalRecord_ShouldThrowArgNull()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.AddMedicalRecord(null));
        }
    }
}
