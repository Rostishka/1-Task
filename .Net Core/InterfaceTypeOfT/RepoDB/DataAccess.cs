using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace RepoDB
{
    class EmployeeDB : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
    }

    public interface IRepository<T> : IDisposable
    {
        void Add(T newEntity);
        void Delete(T existingEntity);
        T FindById(int id);
        IQueryable<T> FindAll();
        int Commit();
    }

    public class SqlRepository<T> : IRepository<T>
    {
        public SqlRepository(DbContext)
        {

        }

        public void Add(T newEntity)
        {
            throw new NotImplementedException();
        }

        public int Commit()
        {
            throw new NotImplementedException();
        }

        public void Delete(T existingEntity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public T FindById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
