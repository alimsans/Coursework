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
            this._context = new HospitalContext();
        }

        protected Controller(DbContextOptions<HospitalContext> options)
        {
            this._context = new HospitalContext(options);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        public void DropDatabase()
        {
            this._context.Database.EnsureDeleted();
        }
    }
}
