using Coursework.DAL;
using Microsoft.EntityFrameworkCore;
using System;

namespace Coursework.BLL
{
    /// <summary>
    /// Class object should be disposed with every use.
    /// </summary>
    public class Controller : IDisposable
    {
        protected HospitalContext _context;

        protected Controller()
        {
            _context = new HospitalContext();
        }

        protected Controller(DbContextOptions<HospitalContext> options)
        {
            _context = new HospitalContext(options);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void DropDatabase()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
