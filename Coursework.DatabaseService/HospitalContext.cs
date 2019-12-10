using Coursework.Types;
using Microsoft.EntityFrameworkCore;


namespace Coursework.DAL
{
    public class HospitalContext : DbContext
    {
        private const string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=ClientDb;Trusted_Connection=True;";

        public DbSet<Doctor> Doctors { get; private set; }
        public DbSet<Patient> Patients { get; private set; }
        public DbSet<Appointment> Appointments { get; private set; }
        public DbSet<MedicalRecord> MedicalRecords { get; private set; }

        public HospitalContext()
            : base()
        {
        }

        public HospitalContext(DbContextOptions<HospitalContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.Appointments)
                .WithOne(a => a.Doctor)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Appointments)
                .WithOne(p => p.Patient)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.MedicalRecords)
                .WithOne(m => m.Patient)
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }
    }
}
