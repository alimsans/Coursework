using Coursework.BLL;
using Coursework.DAL;
using Coursework.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace Coursework.Tests
{
    public class PatientControllerTests : IDisposable
    {
        private DbContextOptions<HospitalContext> _options;
        private PatientController _controller;

        public PatientControllerTests()
        {
            Random random = new Random();
            this._options = new DbContextOptionsBuilder<HospitalContext>()
                .UseInMemoryDatabase(databaseName: "HospitalTestDb" + random.Next(int.MinValue, int.MaxValue))
                .Options;

            this._controller = new PatientController(this._options);
        }

        public void Dispose()
        {
            this._controller.DropDatabase();
            this._controller.Dispose();
        }

        [Fact]
        public void AddPatient_ShouldAdd()
        {
            Patient expected = new Patient("Foo", "To Add");
            Patient actual;

            this._controller.AddPatient(expected);
            actual = this._controller.GetPatient(expected.Id);

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void RemovePatient_ShouldRemove()
        {
            Patient patient = new Patient("Foot", "To remove");
            this._controller.AddPatient(patient);

            Assert.NotNull(this._controller.GetPatient(patient.Id));

            this._controller.RemovePatient(patient);

            Assert.Null(this._controller.GetPatient(patient.Id));
        }

        [Fact]
        public void AlterPatientInfo_ShouldAlter()
        {
            Patient oldPatient = new Patient("Foo", "To alter");
            this._controller.AddPatient(oldPatient);

            Assert.Equal(oldPatient, this._controller.GetPatient(oldPatient.Id));

            Patient newPatientExpected = new Patient("Foo", "Altered") { Id = oldPatient.Id };
            this._controller.AlterPatientInfo(oldPatient, newPatientExpected);
            Patient newPatientActual = this._controller.GetPatient(oldPatient.Id);

            Assert.Equal(newPatientExpected, newPatientActual);
        }

        [Fact]
        public void GetPatient_ShouldGetById()
        {
            Patient expected = new Patient("Foo", "To get by id");
            this._controller.AddPatient(expected);

            Patient actual = this._controller.GetPatient(expected.Id);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetPatientByName_ShouldGetByName()
        {
            Patient expected = new Patient("Foo", "To get by name");
            this._controller.AddPatient(expected);

            ICollection<Patient> actual = this._controller.GetPatientsByName(expected.FirstName, expected.LastName);

            Assert.True(actual.Contains(expected));
        }

        [Fact]
        public void AlterPatientInfo_ThrowsArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => this._controller.AlterPatientInfo(null, new Patient()));
            Assert.Throws<ArgumentNullException>(() => this._controller.AlterPatientInfo(new Patient(), null));
        }

        [Fact]
        public void GetPatients_ShouldGetAllPatients()
        {
            List<Patient> expected = new List<Patient>
            {
                new Patient("Foo", "Patient to get 0"),
                new Patient("Foo", "Patient to get 1"),
                new Patient("Foo", "Patient to get 2")
            };

            foreach (var patient in expected)
            {
                this._controller.AddPatient(patient);
            }

            List<Patient> actual = (List<Patient>)this._controller.GetPatients();

            Assert.True(expected.SequenceEqual(actual));
        }
    }
}
