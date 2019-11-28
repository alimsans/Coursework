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
    public class AppointmentControllerTests : IDisposable
    {
        private DbContextOptions<HospitalContext> _options;
        private AppointmentController _controller;

        public AppointmentControllerTests()
        {
            Random random = new Random();
            this._options = new DbContextOptionsBuilder<HospitalContext>()
                .UseInMemoryDatabase(databaseName: "HospitalTestDb" + random.Next(int.MinValue, int.MaxValue))
                .Options;

            this._controller = new AppointmentController(this._options);
        }

        public void Dispose()
        {
            this._controller.DropDatabase();
            this._controller.Dispose();
        }


        [Fact]
        public void AddAppointment_ShouldAdd()
        {
            Patient patient = new Patient("Foo", "Patient to add");
            Doctor doctor = new Doctor("Foo", "Doctor to add", "Numb");
            Appointment expected = new Appointment(doctor, patient);

            this._controller.AddAppointment(expected);

            Appointment actual = this._controller.GetAppointment(expected.Id);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemoveAppointment_ShouldRemove()
        {
            Patient patient = new Patient("Foo", "Patient to remove");
            Doctor doctor = new Doctor("Foo", "Doctor to remove", "Numb");
            Appointment toRemove = new Appointment(doctor, patient);

            this._controller.AddAppointment(toRemove);

            Assert.NotNull(this._controller.GetAppointment(toRemove.Id));

            this._controller.RemoveAppointment(toRemove);

            Assert.Null(this._controller.GetAppointment(toRemove.Id));
        }

        [Fact]
        public void GetAppointments_ShouldGetAll()
        {
            Random random = new Random();
            List<Doctor> expDoctors = new List<Doctor>
            {
                new Doctor("FooDoc", "To get", "0"),
                new Doctor("FooDoc", "To get", "1"),
                new Doctor("FooDoc", "To get", "2")
            };
            List<Patient> expPatients = new List<Patient>
            {
                new Patient("FooPatient", "To get0"),
                new Patient("FooPatient", "To get1"),
                new Patient("FooPatient", "To get3"),
                new Patient("FooPatient", "To get4"),
                new Patient("FooPatient", "To get5")
            };
            ICollection<Appointment> expAppointments = new List<Appointment>
            {
                //randomized
                new Appointment(expDoctors[random.Next(0, expDoctors.Count-1)], expPatients[random.Next(0, expPatients.Count-1)]),
                new Appointment(expDoctors[random.Next(0, expDoctors.Count-1)], expPatients[random.Next(0, expPatients.Count-1)]),
                new Appointment(expDoctors[random.Next(0, expDoctors.Count-1)], expPatients[random.Next(0, expPatients.Count-1)])
            };
            ICollection<Appointment> actualAppointments = new List<Appointment>();

            Assert.True(_controller.GetAppointments().Count == 0);
            foreach (var appointment in expAppointments)
            {
                _controller.AddAppointment(appointment);
            }

            actualAppointments = _controller.GetAppointments();

            Assert.True(expAppointments.SequenceEqual(actualAppointments));
        }

        [Fact]
        public void AlterAppointment_ShouldAlter()
        {
            Doctor oldDoctor = new Doctor("Foo", "Doc old", "Numb to alter");
            Patient oldPatient = new Patient("Foo", "Patient old");
            Appointment oldAppointment = new Appointment(oldDoctor, oldPatient);
            _controller.AddAppointment(oldAppointment);

            Assert.Equal(oldAppointment, _controller.GetAppointment(oldAppointment.Id));

            Doctor newDoctor = new Doctor("Foo", "Doc new", "Numb altered");
            Patient newPatient = new Patient("Foo", "Patient new");
            Appointment newAppointment = new Appointment(newDoctor, newPatient) { Id = oldAppointment.Id };
            _controller.AlterAppointment(oldAppointment, newAppointment);

            Assert.Equal(newAppointment, _controller.GetAppointment(oldAppointment.Id));
        }

        [Fact]
        public void GetAppointments_ShouldGetOnDay()
        {
            Patient patient = new Patient("Foo", "Patient");
            Doctor doctor = new Doctor("Foo", "Doc", "Numb");
            Appointment[] appointments = new Appointment[]
            {
                new Appointment(doctor, patient, DateTime.Parse("2019-10-10")),
                new Appointment(doctor, patient, DateTime.Parse("2019-11-11 11:11:11")),
                new Appointment(doctor, patient, DateTime.Parse("2019-11-11 12:12:12")),
                new Appointment(doctor, patient, DateTime.Parse("2019-12-12"))
            };

            foreach (var appointment in appointments)
            {
                _controller.AddAppointment(appointment);
            }

            List<Appointment> actual = (List<Appointment>)_controller.GetAppointments(DateTime.Parse("2019-11-11"));

            Assert.Equal(appointments[1], actual[0]);
            Assert.Equal(appointments[2], actual[1]);
        }
    }
}
