using System;
using DAL.Repositories;

namespace DAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly UniversityDbContext _context = new UniversityDbContext();
        private StudentRepository _studetRepository;

        public StudentRepository StudetRepository//Property of seflcreated class
        {
            get
            {
                if (this._studetRepository == null)
                {
                    this._studetRepository = new StudentRepository(_context);
                }
                return _studetRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
