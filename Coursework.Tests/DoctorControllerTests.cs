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
    public class DoctorControllerTests : IDisposable
    {
        private DbContextOptions<HospitalContext> _options;
        private DoctorController _controller;

        public DoctorControllerTests()
        {
            Random random = new Random();
            this._options = new DbContextOptionsBuilder<HospitalContext>()
                .UseInMemoryDatabase(databaseName: "HospitalTestDb" + random.Next(int.MinValue, int.MaxValue))
                .Options;

            this._controller = new DoctorController(this._options);
        }

        public void Dispose()
        {
            this._controller.DropDatabase();
            this._controller.Dispose();
        }

        [Fact]
        public void AddDoctor_ShouldAdd()
        {
            Doctor expected = new Doctor("Foo", "To Add", "Numb");
            Doctor actual;

            this._controller.AddDoctor(expected);
            actual = this._controller.GetDoctor(expected.Id);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddDoctor_ShouldThrowNullArg()
        {
            Assert.Throws<ArgumentNullException>(() => this._controller.AddDoctor(null));
        }

        [Fact]
        public void RemoveDoctor_ShouldRemove()
        {
            Doctor doctor = new Doctor("Foot", "To remove", "Numb");
            this._controller.AddDoctor(doctor);

            Assert.NotNull(this._controller.GetDoctor(doctor.Id));

            this._controller.RemoveDoctor(doctor);

            Assert.Null(this._controller.GetDoctor(doctor.Id));
        }

        [Fact]
        public void AlterDoctorInfo_ShouldAlter()
        {
            Doctor oldDoctor = new Doctor("Foo", "To alter", "Numb");
            this._controller.AddDoctor(oldDoctor);

            Assert.Equal(oldDoctor, this._controller.GetDoctor(oldDoctor.Id));

            Doctor newDoctorExpected = new Doctor("Foo", "Altered", "Doctor") { Id = oldDoctor.Id };
            this._controller.AlterDoctorInfo(oldDoctor, newDoctorExpected);
            Doctor newPatientActual = this._controller.GetDoctor(oldDoctor.Id);

            Assert.Equal(newDoctorExpected, newPatientActual);
        }

        [Fact]
        public void AlterDoctorInfo_ThrowsNullArg()
        {
            Assert.Throws<ArgumentNullException>(() => this._controller.AlterDoctorInfo(new Doctor(), null));
            Assert.Throws<ArgumentNullException>(() => this._controller.AlterDoctorInfo(null, new Doctor()));
        }

        [Fact]
        public void GetDoctor_ShouldGetById()
        {
            Doctor expected = new Doctor("Foo", "To get by id", "Numb");
            this._controller.AddDoctor(expected);

            Doctor actual = this._controller.GetDoctor(expected.Id);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDoctorByName_ShouldGetByName()
        {
            Doctor expected = new Doctor("Foo", "To get by name", "Numb");
            this._controller.AddDoctor(expected);

            ICollection<Doctor> actual = this._controller.GetDoctorsByName(expected.FirstName, expected.LastName);

            Assert.True(actual.Contains(expected));
        }

        [Fact]
        public void GetDoctorByOccupation_ShouldGetByOcc()
        {
            Doctor expected = new Doctor("Foo", "To get by occupation", "Numb");
            this._controller.AddDoctor(expected);

            ICollection<Doctor> actual = this._controller.GetDoctorsByOccupation(expected.Occupation);

            Assert.True(actual.Contains(expected));
        }

        [Fact]
        public void GetDoctors_ShouldGetAllDoctors()
        {
            List<Doctor> expected = new List<Doctor>
            {
                new Doctor("Foo", "Doctor to get 0", "Numb0"),
                new Doctor("Foo", "Doctor to get 1", "Numb1"),
                new Doctor("Foo", "Doctor to get 2", "Numb2")
            };

            foreach (var doctor in expected)
            {
                this._controller.AddDoctor(doctor);
            }

            List<Doctor> actual = (List<Doctor>)this._controller.GetDoctors();

            Assert.True(expected.SequenceEqual(actual));
        }

    }
}
