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
            _options = new DbContextOptionsBuilder<HospitalContext>()
                .UseInMemoryDatabase(databaseName: "HospitalTestDb" + random.Next(int.MinValue, int.MaxValue))
                .Options;

            _controller = new PatientController(_options);
        }

        public void Dispose()
        {
            _controller.DropDatabase();
            _controller.Dispose();
        }

        [Fact]
        public void AddPatient_ShouldAdd()
        {
            Patient expected = new Patient("Foo", "To Add");
            Patient actual;

            _controller.AddPatient(expected);
            actual = _controller.GetPatient(expected.Id);

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void RemovePatient_ShouldRemove()
        {
            Patient patient = new Patient("Foot", "To remove");
            _controller.AddPatient(patient);

            Assert.NotNull(_controller.GetPatient(patient.Id));

            _controller.RemovePatient(patient);

            Assert.Null(_controller.GetPatient(patient.Id));
        }

        [Fact]
        public void AlterPatientInfo_ShouldAlter()
        {
            Patient oldPatient = new Patient("Foo", "To alter");
            _controller.AddPatient(oldPatient);

            Assert.Equal(oldPatient, _controller.GetPatient(oldPatient.Id));

            Patient newPatientExpected = new Patient("Foo", "Altered") { Id = oldPatient.Id };
            _controller.AlterPatientInfo(oldPatient, newPatientExpected);
            Patient newPatientActual = _controller.GetPatient(oldPatient.Id);

            Assert.Equal(newPatientExpected, newPatientActual);
        }

        [Fact]
        public void GetPatient_ShouldGetById()
        {
            Patient expected = new Patient("Foo", "To get by id");
            _controller.AddPatient(expected);

            Patient actual = _controller.GetPatient(expected.Id);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetPatientByName_ShouldGetByName()
        {
            Patient expected = new Patient("Foo", "To get by name");
            _controller.AddPatient(expected);

            ICollection<Patient> actual = _controller.GetPatientsByName(expected.FirstName, expected.LastName);

            Assert.True(actual.Contains(expected));
        }

        [Fact]
        public void AlterPatientInfo_ThrowsArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.AlterPatientInfo(null, new Patient()));
            Assert.Throws<ArgumentNullException>(() => _controller.AlterPatientInfo(new Patient(), null));
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
                _controller.AddPatient(patient);
            }

            List<Patient> actual = (List<Patient>)_controller.GetPatients();

            Assert.True(expected.SequenceEqual(actual));
        }
    }
}
